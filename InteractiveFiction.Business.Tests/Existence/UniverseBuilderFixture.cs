using InteractiveFiction.Business.Entity;
using InteractiveFiction.Business.Entity.AnimateEntities;
using InteractiveFiction.Business.Entity.Locations;
using InteractiveFiction.Business.Existence;
using InteractiveFiction.Business.Goal.Questing;
using InteractiveFiction.Business.Goal;
using InteractiveFiction.Business.Tests.Utils;
using Moq;
using System.IO.Abstractions;

namespace InteractiveFiction.Business.Tests.Existence
{
    public class UniverseBuilderFixture
    {
        private IUniverse? universe;
        private IList<string> arboraLocations = new List<string>() { "KoboldDen", "BorealForest", "Heath", "OldForest", "RuinedVillage", "Spawn", "Arbora" };
        private string arboraMap =
            "{\"BorealForest\": \"West;Heath,North;RuinedVillage\",\"RuinedVillage\": \"South;BorealForest,North;OldForest\"," +
            "\"OldForest\": \"South;RuinedVillage,In;KoboldDen\",\"KoboldDen\": \"Out;OldForest\",\"Heath\": \"East;BorealForest\"," +
            "\"Spawn\": \"West;OldForest\"}";
        private IList<string> erogundLocations = new List<string>() { "MistyCastle", "OldFarmstead", "QuaintVillage", "Spawn", "Erogund" };
        private string erogundMap =
            "{\"QuaintVillage\": \"North;OldFarmstead,East;MistyCastle\",\"OldFarmstead\": \"South;QuaintVillage,SouthEast;MistyCastle\"," +
            "\"MistyCastle\": \"West;QuaintVillage,NorthWest;OldFarmstead\",\"Spawn\": \"West;MistyCastle\"}";
        private string region;
        private IList<string> regionLocations;
        private string regionMap;

        private UniverseBuilderFixture() { }

        public static UniverseBuilderFixture GetFixture() { return new UniverseBuilderFixture(); }

        public UniverseBuilderFixture ForRegion(string region)
        {
            this.region = region;

            if (region.Equals("Arbora"))
            {
                regionLocations = arboraLocations;
                regionMap = arboraMap;
            } 
            else if (region.Equals("Erogund"))
            {
                regionLocations = erogundLocations;
                regionMap = erogundMap;
            }
            
            return this;
        }

        public UniverseBuilderFixture CreateUniverse()
        {
            var universeBuilder = new UniverseBuilder(
                GetEntityBuilderMock().Object,
                GetFileSystemMock().Object);

            universe = universeBuilder.Create(region);

            return this;
        }

        public void AssertErogundLocationsPlacedProperly()
        {
            Assert.NotNull(universe);
            var spawn = universe.GetInstant().Spawn;
            Assert.NotNull(spawn);
            Assert.Single(spawn.Paths);
            var quaintVillage = GetLocation(spawn, "QuaintVillage");
            Assert.Equal(GetLocation(spawn, "OldFarmstead"), quaintVillage.Go(Direction.North));

        }

        public void AssertCastleHasKing()
        {
            var mistyCastle = GetLocation(universe.GetInstant().Spawn, "MistyCastle");            
            Assert.NotEmpty(mistyCastle.Children);
            Assert.IsType<Character>(mistyCastle.Children[0]);
        }

        public void AssertArboraLocationsPlacedProperly()
        {
            Assert.NotNull(universe);
            var spawn = universe.GetInstant().Spawn;
            Assert.NotNull(spawn);
            Assert.Single(spawn.Paths);
            var borealForest = GetLocation(spawn, "BorealForest");
            Assert.Equal(GetLocation(spawn, "Heath"), borealForest.Go(Direction.West));
        }

        public void AssertDenHasKobolds()
        {
            Assert.NotNull(universe);

            var child = GetLocation(universe.GetInstant().Spawn, "KoboldDen");
            Assert.IsType<Character>(child.Children[0]);
        }

        private static Location GetLocation(Location current, string title, List<Guid>? visited = null)
        {
            visited ??= new List<Guid>();
            if (visited.Contains(current.Id))
            {
                return NullLocation.Instance;
            }
            else
            {
                visited.Add(current.Id);
            }

            if (current.Title == title)
            {
                return current;
            }

            Location found = NullLocation.Instance;
            foreach (Location sibling in current.Paths.Values)
            {
                var recursiveFound = GetLocation(sibling, title, visited);
                if (recursiveFound != NullLocation.Instance)
                {
                    found = recursiveFound;
                }
            }

            return found;
        }

        private Mock<IFileSystem> GetFileSystemMock()
        {
            var directory = new Mock<IDirectory>();
            directory.Setup(x => x.Exists("./games/Arbora")).Returns(true);
            directory.Setup(x => x.Exists("./games/Erogund")).Returns(true);

            var file = new Mock<IFile>();
            file.Setup(x => x.ReadAllText(It.Is<string>(y => y.EndsWith("_map.json")))).Returns(regionMap);
            file.Setup(x => x.Exists(It.Is<string>(y => y.EndsWith("_map.json")))).Returns(true);
            file.Setup(x => x.Exists(It.Is<string>(y => y.EndsWith("Kobold.json")))).Returns(true);
            file.Setup(x => x.Exists(It.Is<string>(y => y.EndsWith("KingLeon.json")))).Returns(true);
            foreach (string location in regionLocations)
            {
                file.Setup(x => x.Exists(It.Is<string>(y => y.EndsWith(location + ".json")))).Returns(true);
            }

            var fs = new Mock<IFileSystem>();
            fs.Setup(x => x.File).Returns(file.Object);
            fs.Setup(x => x.Directory).Returns(directory.Object);

            return fs;
        }

        private Mock<IEntityBuilderFactory> GetEntityBuilderMock()
        {
            var builder = new Mock<IEntityBuilder>();

            foreach (string location in regionLocations)
            {
                if (location.Equals("KoboldDen"))
                {
                    SetupLocationForBuilder(builder, region, location, new List<string>() { "Kobold" });
                    SetupCharacterForBuilder(builder, "Kobold");
                } 
                else if (location.Equals("MistyCastle"))
                {

                    SetupLocationForBuilder(builder, region, location, new List<string>() { "KingLeon" });
                    SetupCharacterForBuilder(builder, "KingLeon");
                }
                else
                {
                    SetupLocationForBuilder(builder, region, location);
                }
            }

            return DefaultMocks.GetEntityBuilderFactoryMock(builder);
        }


        private static void SetupLocationForBuilder(Mock<IEntityBuilder> locationBuilder, string path, string name, List<string>? entityNames = null)
        {
            var nestedBuilder = new Mock<IEntityBuilder>();
            nestedBuilder.Setup(_ => _.Build()).Returns(
                new Location(DefaultMocks.GetTextDecorator().Object)
                {
                    Title = name,
                    EntityNames = entityNames ?? new List<string>()
                });
            locationBuilder
                .Setup(_ => _.From(It.Is<string>(_ => _.Contains(path + "/" + name))))
                .Returns(nestedBuilder.Object);
        }

        private static void SetupCharacterForBuilder(Mock<IEntityBuilder> locationBuilder, string name)
        {
            var nestedBuilder = new Mock<IEntityBuilder>();
            nestedBuilder.Setup(_ => _.Build()).Returns(
                new Character(
                    new Mock<IQuestManager>().Object,
                    new Mock<IObserver<IStat>>().Object,
                    DefaultMocks.GetProcedureBuilderMock().Object)
                {
                    Name = name,
                });

            locationBuilder
                .Setup(_ => _.From(It.Is<string>(_ => _.Contains("Entity/" + name))))
                .Returns(nestedBuilder.Object);
        }
    }
}
