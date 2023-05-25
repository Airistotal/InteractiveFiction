using InteractiveFiction.Business.Entity;
using InteractiveFiction.Business.Entity.Locations;

namespace InteractiveFiction.Business.Existence
{
    public class UniverseBuilder : IUniverseBuilder
    {
        private Location? root;
        private Location? spawn;
        private string? baseDir;
        private readonly Dictionary<string, Location> loadedLocations = new();
        private readonly IEntityBuilderFactory entityBuilderFactory;

        public UniverseBuilder(IEntityBuilderFactory entityBuilderFactory)
        {
            this.entityBuilderFactory = entityBuilderFactory;
        }

        public IUniverse Create(string name)
        {
            baseDir = "./games/" + name;
            if (!Directory.Exists(baseDir))
            {
                throw new Exception("Unable to find Universe to load.");
            }

            root = LoadLocationFromFile(name);

            LoadMap();
            if (spawn == null)
            {
                throw new NoSpawnSetException();
            }

            root.Children.AddRange(loadedLocations.Values);

            return new Universe(spawn);
        }

        private void LoadMap()
        {
            var mapInfos = ReadMapInfo();

            foreach (string[] mapInfo in mapInfos)
            {
                var origin = LoadLocation(mapInfo[0]);
                if (mapInfo[0].Equals("Spawn"))
                {
                    spawn = origin;
                }

                foreach (var destinationInfo in mapInfo[1].Split(','))
                {
                    var split = destinationInfo.Split(";");
                    var direction = (Direction)Enum.Parse(typeof(Direction), split[0]);
                    var destination = LoadLocation(split[1]);

                    origin.AddPath(direction, destination);
                }
            }
        }

        private List<string[]> ReadMapInfo()
        {
            var filePath = baseDir + "/_map.txt";
            var children = new List<string[]>();
            if (!File.Exists(filePath))
            {
                return children;
            }

            foreach (string line in File.ReadLines(filePath))
            {
                children.Add(line.Split(":"));
            }

            return children;
        }

        private Location LoadLocation(string name)
        {
            if (!loadedLocations.ContainsKey(name))
            {
                loadedLocations.Add(name, LoadLocationFromFile(name));
            }

            return loadedLocations[name];
        }

        private Location LoadLocationFromFile(string fileName)
        {
            var fileLocation = baseDir + "/" + fileName + ".txt";
            if (!File.Exists(fileLocation))
            {
                throw new Exception("Unable to load location " + fileLocation);
            }

            var location = (Location)entityBuilderFactory.GetBuilder().FromLines(File.ReadLines(fileLocation)).Build();

            LoadEntities(location);

            return location;
        }

        private void LoadEntities(Location location)
        {
            foreach (var entity in location.EntityNames)
            {
                location.Children.Add(LoadEntity(entity));
            }
        }

        private IEntity LoadEntity(string entityName)
        {
            var fileLocation = baseDir + "/Entity/" + entityName + ".txt";
            if (!File.Exists(fileLocation))
            {
                throw new Exception("Unable to load entity " + fileLocation);
            }

            return entityBuilderFactory.GetBuilder().FromLines(File.ReadLines(fileLocation)).Build();
        }
    }
}
