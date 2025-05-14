using InteractiveFiction.Business.Entity;
using InteractiveFiction.Business.Entity.Locations;
using Newtonsoft.Json.Linq;
using System.IO.Abstractions;

namespace InteractiveFiction.Business.Existence
{
    public class UniverseBuilder : IUniverseBuilder
    {
        private Location? root;
        private Location? spawn;
        private string? baseDir;
        private readonly Dictionary<string, Location> loadedLocations = new();

        private readonly IEntityBuilderFactory entityBuilderFactory;
        private readonly IFileSystem fileSystem;

        public UniverseBuilder(IEntityBuilderFactory entityBuilderFactory, IFileSystem fileSystem)
        {
            this.entityBuilderFactory = entityBuilderFactory;
            this.fileSystem = fileSystem;
        }

        public IUniverse Create(string name)
        {
            baseDir = "./games/" + name;
            if (!fileSystem.Directory.Exists(baseDir))
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
            var filePath = baseDir + "/_map.json";
            if (!fileSystem.File.Exists(filePath))
            {
                return new List<string[]>();
            }

            var map = JObject.Parse(fileSystem.File.ReadAllText(filePath));
            var children = new List<string[]>();

            foreach (var mapInfo in map)
            {
                children.Add(new string[] { mapInfo.Key, mapInfo.Value.Value<string>() });
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
            var fileLocation = baseDir + "/" + fileName + ".json";
            if (!fileSystem.File.Exists(fileLocation))
            {
                throw new Exception("Unable to find " + Directory.GetCurrentDirectory() + fileLocation);
            }

            var location = (Location)entityBuilderFactory.GetBuilder().From(fileLocation).Build();

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
            var fileLocation = baseDir + "/Entity/" + entityName + ".json";
            if (!fileSystem.File.Exists(fileLocation))
            {
                throw new Exception("Unable to load entity " + fileLocation);
            }

            return entityBuilderFactory.GetBuilder().From(fileLocation).Build();
        }
    }
}
