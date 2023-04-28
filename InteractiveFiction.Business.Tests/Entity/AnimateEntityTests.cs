using InteractiveFiction.Business.Entity;
using InteractiveFiction.Business.Existence;
using InteractiveFiction.Business.Procedure;
using InteractiveFiction.Business.Tests.Utils;
using Moq;

namespace InteractiveFiction.Business.Tests.Entity
{
    public class AnimateEntityTests
    {
        [Fact]
        public void WhenAnimateEntityCreatedHasPhysicalAttributes()
        {
            var sut = new TestAnimateEntity();

            Assert.Equal(0, sut.Health);
            Assert.Equal(0, sut.Strength);
            Assert.Equal(0, sut.Dexterity);
            Assert.Equal(0, sut.Endurance);
            Assert.Equal(0, sut.Speed);
        }

        [Fact]
        public void WhenAnimateEntityCreatedHasMentalAttributes()
        {
            var sut = new TestAnimateEntity();

            Assert.Equal(0, sut.Restraint);
            Assert.Equal(0, sut.Discretion);
            Assert.Equal(0, sut.Courage);
            Assert.Equal(0, sut.Fairness);
            Assert.Equal(0, sut.Compassion);
            Assert.Equal(0, sut.Hope);
            Assert.Equal(0, sut.Groundedness);
        }

        [Fact]
        public void WhenAnimateEntityCreatedHasIdentityAttributes()
        {
            var sut = new TestAnimateEntity();

            Assert.Equal("", sut.Name);
            Assert.Equal("", sut.Description);
            Assert.Equal("", sut.Birthdate);
            Assert.Empty(sut.Parents);
            Assert.Empty(sut.Children);
            Assert.Null(sut.Location);
        }
        [Fact]
        public void WhenAnimateEntityMovesWithLocationPutsProcedureIntoUniverse()
        {
            var universe = new Mock<IUniverse>();
            universe.Setup(_ => _.Put(It.IsAny<IProcedure>()));
            var sut = new TestAnimateEntity()
            {
                Universe = universe.Object,
            };
            sut.SetLocation(Location.Empty());

            sut.Perform(ProcedureType.Move, new[] { "" });

            universe.Verify(_ => _.Put(It.IsAny<IProcedure>()), Times.Once);
        }

        [Fact]
        public void WhenAnimateEntityMovesWithoutLocationIgnores()
        {
            var universe = new Mock<IUniverse>();
            var sut = new TestAnimateEntity()
            {
                Universe = universe.Object,
            };
            universe.Setup(_ => _.Put(It.IsAny<IProcedure>()));

            sut.Perform(ProcedureType.Move, new[] { "" });

            universe.Verify(_ => _.Put(It.IsAny<IProcedure>()), Times.Never);
        }

        [Fact]
        public void WhenAnimateEntityChangesLocationUpdatesMoveProcedure()
        {
            var sut = new TestAnimateEntity();
            var location2 = new Location(DefaultMocks.GetProcedureBuilderMock().Object);

            sut.SetLocation(Location.Empty());
            sut.SetLocation(location2);

            sut.builderMock.Verify(_ => _.target(location2), Times.Once);
        }
    }
}
