using InteractiveFiction.Business.Entity;
using InteractiveFiction.Business.Existence;
using InteractiveFiction.Business.Tests.Utils;
using Moq;

namespace InteractiveFiction.Business.Tests.Existence
{
    public class UniverseLoaderTests
    {
        [Fact]
        public void WhenCreateErogundLoadsLocation()
        {
            var universeLoader = new UniverseLoader(GetEntityBuilderMockWithErogundLocations().Object);

            var universe = universeLoader.Create("Erogund");

            Assert.NotNull(universe);
            Instant instant = universe.GetInstant();
            Assert.NotNull(instant);
            Assert.NotNull(instant.Root);
            Assert.IsType<Location>(instant.Root);
        }

        [Fact]
        public void WhenCreateErogundPlacesLocations()
        {
            var universeLoader = new UniverseLoader(GetEntityBuilderMockWithErogundLocations().Object);

            var universe = universeLoader.Create("Erogund");

            Assert.NotNull(universe);
            var instant = universe.GetInstant();
            Assert.NotNull(instant);
            Assert.NotNull(instant.Root);
            Assert.IsType<Location>(instant.Root);
            var root = (Location)instant.Root;
            Assert.Equal(3, root.Children.Count);
            var quaintVillage = getLocation(root, "Quaint Village");
            Assert.Equal(root, quaintVillage.Location);
            Assert.Equal(getLocation(root, "Old Farmstead"), quaintVillage.Go(Direction.North));
        }

        [Fact]
        public void WhenCreateErogundPlacesAnimateEntities()
        {
            var universeLoader = new UniverseLoader(GetEntityBuilderMockWithErogundLocations().Object);

            var universe = universeLoader.Create("Erogund");
            var instant = universe.GetInstant();
            var root = (Location)instant.Root;
            var child = getLocation(root, "Misty Castle");

            Assert.IsType<Character>(child.Children[0]);
        }

        [Fact]
        public void WhenCreateArboraLoadsLocation()
        {
            var universeLoader = new UniverseLoader(GetEntityBuilderMockWithArboraLocations().Object);

            var universe = universeLoader.Create("Arbora");

            Assert.NotNull(universe);
            var instant = universe.GetInstant();
            Assert.NotNull(instant);
            Assert.NotNull(instant.Root);
            Assert.IsType<Location>(instant.Root);
        }

        [Fact]
        public void WhenCreateArboraPlacesLocations()
        {
            var universeLoader = new UniverseLoader(GetEntityBuilderMockWithArboraLocations().Object);

            var universe = universeLoader.Create("Arbora");

            Assert.NotNull(universe);
            var instant = universe.GetInstant();
            Assert.NotNull(instant);
            Assert.NotNull(instant.Root);
            Assert.IsType<Location>(instant.Root);
            var root = (Location)instant.Root;
            Assert.Equal(5, root.Children.Count);
            var borealForest = getLocation(root, "Boreal Forest");
            Assert.Equal(root, borealForest.Location);
            Assert.Equal(getLocation(root, "Heath"), borealForest.Go(Direction.West));
        }

        [Fact]
        public void WhenCreateArboraPlacesAnimateEntities()
        {
            var universeLoader = new UniverseLoader(GetEntityBuilderMockWithArboraLocations().Object);

            var universe = universeLoader.Create("Arbora");
            var instant = universe.GetInstant();
            var root = (Location)instant.Root;
            var child = getLocation(root, "Kobold Den");

            Assert.IsType<Character>(child.Children[0]);
        }

        private Location getLocation(Location root, string Title)
        {
            foreach (Location location in root.Children)
            {
                if (location.Title == Title)
                {
                    return location;
                }
            }

            throw new Exception("Unable to find location " + Title);
        }

        private Mock<IEntityBuilderFactory> GetEntityBuilderMockWithArboraLocations()
        {
            var koboldDenBuilder = new Mock<IEntityBuilder>();
            koboldDenBuilder.Setup(_ => _.Build()).Returns(new Location(DefaultMocks.GetProcedureBuilderMock().Object)
            {
                Title="Kobold Den",
                EntityNames = new List<string>() { "Kobold" }
            });

            var borealForestBuilder = new Mock<IEntityBuilder>();
            borealForestBuilder.Setup(_ => _.Build()).Returns(new Location(DefaultMocks.GetProcedureBuilderMock().Object)
            {
                Title = "Boreal Forest"
            });

            var heathBuilder = new Mock<IEntityBuilder>();
            heathBuilder.Setup(_ => _.Build()).Returns(new Location(DefaultMocks.GetProcedureBuilderMock().Object)
            {
                Title = "Heath"
            });

            var oldForestBuilder = new Mock<IEntityBuilder>();
            oldForestBuilder.Setup(_ => _.Build()).Returns(new Location(DefaultMocks.GetProcedureBuilderMock().Object)
            {
                Title = "Old Forest"
            });

            var ruinedVillageBuilder = new Mock<IEntityBuilder>();
            ruinedVillageBuilder.Setup(_ => _.Build()).Returns(new Location(DefaultMocks.GetProcedureBuilderMock().Object)
            {
                Title = "Ruined Village"
            });

            var arboraBuilder = new Mock<IEntityBuilder>();
            arboraBuilder.Setup(_ => _.Build()).Returns(new Location(DefaultMocks.GetProcedureBuilderMock().Object)
            {
                Title = "Arbora"
            });

            var locationBuilder = new Mock<IEntityBuilder>();
            locationBuilder.Setup(_ => _.FromLines(It.Is<IEnumerable<string>>(_ => _.Any(_ => _.StartsWith("Title:Kobold Den"))))).Returns(koboldDenBuilder.Object);
            locationBuilder.Setup(_ => _.FromLines(It.Is<IEnumerable<string>>(_ => _.Any(_ => _.StartsWith("Title:Boreal Forest"))))).Returns(borealForestBuilder.Object);
            locationBuilder.Setup(_ => _.FromLines(It.Is<IEnumerable<string>>(_ => _.Any(_ => _.StartsWith("Title:Heath"))))).Returns(heathBuilder.Object);
            locationBuilder.Setup(_ => _.FromLines(It.Is<IEnumerable<string>>(_ => _.Any(_ => _.StartsWith("Title:Old Forest"))))).Returns(oldForestBuilder.Object);
            locationBuilder.Setup(_ => _.FromLines(It.Is<IEnumerable<string>>(_ => _.Any(_ => _.StartsWith("Title:Ruined Village"))))).Returns(ruinedVillageBuilder.Object);
            locationBuilder.Setup(_ => _.FromLines(It.Is<IEnumerable<string>>(_ => _.Any(_ => _.StartsWith("Title:Arbora"))))).Returns(arboraBuilder.Object);

            return DefaultMocks.GetEntityBuilderFactoryMock(locationBuilder);
        }

        private Mock<IEntityBuilderFactory> GetEntityBuilderMockWithErogundLocations()
        {
            var mistyCastleBuilder = new Mock<IEntityBuilder>();
            mistyCastleBuilder.Setup(_ => _.Build()).Returns(new Location(DefaultMocks.GetProcedureBuilderMock().Object)
            {
                Title = "Misty Castle",
                EntityNames = new List<string>() { "KingLeon" }
            });

            var oldFarmsteadBuilder = new Mock<IEntityBuilder>();
            oldFarmsteadBuilder.Setup(_ => _.Build()).Returns(new Location(DefaultMocks.GetProcedureBuilderMock().Object)
            {
                Title = "Old Farmstead"
            });

            var quaintVillageBuilder = new Mock<IEntityBuilder>();
            quaintVillageBuilder.Setup(_ => _.Build()).Returns(new Location(DefaultMocks.GetProcedureBuilderMock().Object)
            {
                Title = "Quaint Village"
            });

            var erogundBuilder = new Mock<IEntityBuilder>();
            erogundBuilder.Setup(_ => _.Build()).Returns(new Location(DefaultMocks.GetProcedureBuilderMock().Object)
            {
                Title = "Erogund"
            });

            var locationBuilder = new Mock<IEntityBuilder>();
            locationBuilder.Setup(_ => _.FromLines(It.Is<IEnumerable<string>>(_ => _.Any(_ => _.Contains("Title:Misty Castle"))))).Returns(mistyCastleBuilder.Object);
            locationBuilder.Setup(_ => _.FromLines(It.Is<IEnumerable<string>>(_ => _.Any(_ => _.Contains("Title:Old Farmstead"))))).Returns(oldFarmsteadBuilder.Object);
            locationBuilder.Setup(_ => _.FromLines(It.Is<IEnumerable<string>>(_ => _.Any(_ => _.Contains("Title:Quaint Village"))))).Returns(quaintVillageBuilder.Object);
            locationBuilder.Setup(_ => _.FromLines(It.Is<IEnumerable<string>>(_ => _.Any(_ => _.Contains("Title:Erogund"))))).Returns(erogundBuilder.Object);

            return DefaultMocks.GetEntityBuilderFactoryMock(locationBuilder);
        }
    }
}
