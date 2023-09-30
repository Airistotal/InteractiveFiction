using InteractiveFiction.Business.Entity;
using InteractiveFiction.Business.Entity.AnimateEntities;
using InteractiveFiction.Business.Entity.Locations;
using InteractiveFiction.Business.Existence;
using InteractiveFiction.Business.Tests.Utils;
using Moq;

namespace InteractiveFiction.Business.Tests.Existence
{
    public class UniverseBuilderTests
    {
        [Fact]
        public void WhenCreateErogundLoadsLocation()
        {
            var universeBuilder = new UniverseBuilder(GetEntityBuilderMockWithErogundLocations().Object);

            var universe = universeBuilder.Create("Erogund");

            Assert.NotNull(universe);
            Instant instant = universe.GetInstant();
            Assert.NotNull(instant);
            Assert.NotNull(instant.Spawn);
        }

        [Fact]
        public void WhenCreateErogundPlacesLocations()
        {
            var universeBuilder = new UniverseBuilder(GetEntityBuilderMockWithErogundLocations().Object);

            var universe = universeBuilder.Create("Erogund");

            var spawn = universe.GetInstant().Spawn;
            Assert.Single(spawn.Paths);
            var quaintVillage = GetLocation(spawn, "QuaintVillage");
            Assert.Equal(GetLocation(spawn, "OldFarmstead"), quaintVillage.Go(Direction.North));
        }

        [Fact]
        public void WhenCreateErogundPlacesAnimateEntities()
        {
            var universeBuilder = new UniverseBuilder(GetEntityBuilderMockWithErogundLocations().Object);

            var universe = universeBuilder.Create("Erogund");
            var mistyCastle = GetLocation(universe.GetInstant().Spawn, "MistyCastle");

            Assert.NotEmpty(mistyCastle.Children);
            Assert.IsType<Character>(mistyCastle.Children[0]);
        }

        [Fact]
        public void WhenCreateArboraLoadsLocation()
        {
            var universeBuilder = new UniverseBuilder(GetEntityBuilderMockWithArboraLocations().Object);

            var universe = universeBuilder.Create("Arbora");

            Assert.NotNull(universe);
            var instant = universe.GetInstant();
            Assert.NotNull(instant);
            Assert.NotNull(instant.Spawn);
        }

        [Fact]
        public void WhenCreateArboraPlacesLocations()
        {
            var universeBuilder = new UniverseBuilder(GetEntityBuilderMockWithArboraLocations().Object);

            var universe = universeBuilder.Create("Arbora");

            var spawn = universe.GetInstant().Spawn;
            Assert.Single(spawn.Paths);
            var borealForest = GetLocation(spawn, "BorealForest");
            Assert.Equal(GetLocation(spawn, "Heath"), borealForest.Go(Direction.West));
        }

        [Fact]
        public void WhenCreateArboraPlacesAnimateEntities()
        {
            var universeBuilder = new UniverseBuilder(GetEntityBuilderMockWithArboraLocations().Object);

            var universe = universeBuilder.Create("Arbora");
            var child = GetLocation(universe.GetInstant().Spawn, "KoboldDen");

            Assert.IsType<Character>(child.Children[0]);
        }

        private static Location GetLocation(Location current, string title, List<Guid>? visited = null)
        {
            if (visited == null) { visited = new List<Guid>(); }
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

        private Mock<IEntityBuilderFactory> GetEntityBuilderMockWithArboraLocations()
        {
            var builder = new Mock<IEntityBuilder>();

            SetupLocationForBuilder(builder, "Arbora", "KoboldDen", new List<string>() { "Kobold" });
            SetupLocationForBuilder(builder, "Arbora", "BorealForest");
            SetupLocationForBuilder(builder, "Arbora", "Heath");
            SetupLocationForBuilder(builder, "Arbora", "OldForest");
            SetupLocationForBuilder(builder, "Arbora", "RuinedVillage");
            SetupLocationForBuilder(builder, "Arbora", "Spawn");
            SetupLocationForBuilder(builder, "Arbora", "Arbora");
            SetupCharacterForBuilder(builder, "Kobold");

            return DefaultMocks.GetEntityBuilderFactoryMock(builder);
        }

        private Mock<IEntityBuilderFactory> GetEntityBuilderMockWithErogundLocations()
        {
            var builder = new Mock<IEntityBuilder>();

            SetupLocationForBuilder(builder, "Erogund", "MistyCastle", new List<string>() { "KingLeon" });
            SetupLocationForBuilder(builder, "Erogund", "OldFarmstead");
            SetupLocationForBuilder(builder, "Erogund", "QuaintVillage");
            SetupLocationForBuilder(builder, "Erogund", "Spawn");
            SetupLocationForBuilder(builder, "Erogund", "Erogund");
            SetupCharacterForBuilder(builder, "KingLeon");

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
                new Character(DefaultMocks.GetProcedureBuilderMock().Object)
                {
                    Name = name,
                });

            locationBuilder
                .Setup(_ => _.From(It.Is<string>(_ => _.Contains("Entity/" + name))))
                .Returns(nestedBuilder.Object);
        }
    }
}
