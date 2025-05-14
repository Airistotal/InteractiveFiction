using Moq;
using InteractiveFiction.Business.Existence;
using InteractiveFiction.Business.Procedure;
using InteractiveFiction.Business.Entity;
using InteractiveFiction.Business.Tests.Utils;
using InteractiveFiction.Business.Entity.Locations;

namespace InteractiveFiction.Business.Tests.Existence
{
    public class UniverseTests
    {
        [Fact]
        public void WhenTickHappensLawApplies()
        {
            var law = new Mock<ILaw>();
            law.Setup(_ => _.apply());
            var universe = new Universe(NullLocation.Instance);
            universe.RegisterLaw(law.Object);

            universe.Tick();

            law.Verify(_ => _.apply(), Times.Once);
        }

        [Fact]
        public void WhenTickHappensExecutesActions()
        {
            var procedure = new Mock<IProcedure>();
            procedure.Setup(_ => _.Perform());
            var universe = new Universe(NullLocation.Instance);
            universe.Put(procedure.Object);

            universe.Tick();

            procedure.Verify(_ => _.Perform(), Times.Once);
        }

        [Fact]
        public void WhenSpawnCharacter_SpawnsCharacter()
        {
            var spawn = new Location(DefaultMocks.GetTextDecorator().Object)
            {
                Title = "Spawn"
            };
            var entity = new Mock<IEntity>();
            var universe = new Universe(spawn);

            universe.Spawn(entity.Object);

            entity.Verify(_ => _.SetUniverse(It.IsAny<IUniverse>()), Times.Once);
            entity.Verify(_ => _.SetLocation(It.IsAny<Location>()), Times.Once);
            entity.Verify(_ => _.AddEvent(It.IsAny<string>()), Times.Once);
            Assert.Contains(entity.Object, spawn.Children);
        }
    }
}