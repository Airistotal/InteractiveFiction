using InteractiveFiction.Business.Entity;
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
            var quaintVillage = GetLocation(spawn, "Quaint Village");
            Assert.Equal(GetLocation(spawn, "Old Farmstead"), quaintVillage.Go(Direction.North));
        }

        [Fact]
        public void WhenCreateErogundPlacesAnimateEntities()
        {
            var universeBuilder = new UniverseBuilder(GetEntityBuilderMockWithErogundLocations().Object);

            var universe = universeBuilder.Create("Erogund");
            var child = GetLocation(universe.GetInstant().Spawn, "Misty Castle");

            Assert.IsType<Character>(child.Children[0]);
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
            var borealForest = GetLocation(spawn, "Boreal Forest");
            Assert.Equal(GetLocation(spawn, "Heath"), borealForest.Go(Direction.West));
        }

        [Fact]
        public void WhenCreateArboraPlacesAnimateEntities()
        {
            var universeBuilder = new UniverseBuilder(GetEntityBuilderMockWithArboraLocations().Object);

            var universe = universeBuilder.Create("Arbora");
            var child = GetLocation(universe.GetInstant().Spawn, "Kobold Den");

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
            var locationBuilder = new Mock<IEntityBuilder>();

            SetupLocationForBuilder(locationBuilder, "Kobold Den", new List<string>() { "Kobold" });
            SetupLocationForBuilder(locationBuilder, "Boreal Forest");
            SetupLocationForBuilder(locationBuilder, "Heath");
            SetupLocationForBuilder(locationBuilder, "Old Forest");
            SetupLocationForBuilder(locationBuilder, "Ruined Village");
            SetupLocationForBuilder(locationBuilder, "Arbora");
            SetupLocationForBuilder(locationBuilder, "Spawn");

            return DefaultMocks.GetEntityBuilderFactoryMock(locationBuilder);
        }

        private Mock<IEntityBuilderFactory> GetEntityBuilderMockWithErogundLocations()
        {
            var locationBuilder = new Mock<IEntityBuilder>();

            SetupLocationForBuilder(locationBuilder, "Misty Castle", new List<string>() { "KingLeon" });
            SetupLocationForBuilder(locationBuilder, "Old Farmstead");
            SetupLocationForBuilder(locationBuilder, "Quaint Village");
            SetupLocationForBuilder(locationBuilder, "Erogund");
            SetupLocationForBuilder(locationBuilder, "Spawn");

            return DefaultMocks.GetEntityBuilderFactoryMock(locationBuilder);
        }

        private void SetupLocationForBuilder(Mock<IEntityBuilder> locationBuilder, string name, List<string>? entityNames = null)
        {
            var nestedBuilder = new Mock<IEntityBuilder>();
            nestedBuilder.Setup(_ => _.Build()).Returns(
                new Location(DefaultMocks.GetTextDecorator().Object)
                {
                    Title = name,
                    EntityNames = entityNames ?? new List<string>()
                });

            locationBuilder
                .Setup(_ => _.FromLines(It.Is<IEnumerable<string>>(_ => _.Any(_ => _.Contains("Title:" + name)))))
                .Returns(nestedBuilder.Object);
        }
    }
}
