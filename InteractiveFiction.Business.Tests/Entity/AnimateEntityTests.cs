using InteractiveFiction.Business.Entity;
using InteractiveFiction.Business.Existence;
using InteractiveFiction.Business.Procedure;
using InteractiveFiction.Business.Procedure.Argument;
using InteractiveFiction.Business.Tests.Utils;
using Moq;

namespace InteractiveFiction.Business.Tests.Entity
{
    public class AnimateEntityTests
    {
        [Fact]
        public void WhenAnimateEntityCreatedHasPhysicalAttributes()
        {
            var sut = GetAnimateEntity();

            Assert.Equal(0, sut.Health);
            Assert.Equal(0, sut.Strength);
            Assert.Equal(0, sut.Dexterity);
            Assert.Equal(0, sut.Endurance);
            Assert.Equal(0, sut.Speed);
        }

        [Fact]
        public void WhenAnimateEntityCreatedHasMentalAttributes()
        {
            var sut = GetAnimateEntity();

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
            var sut = GetAnimateEntity();

            Assert.Equal("", sut.Name);
            Assert.Equal("", sut.Description);
            Assert.Equal("", sut.Birthdate);
            Assert.Empty(sut.Parents);
            Assert.Empty(sut.Children);
            Assert.Equal(NullLocation.Instance, sut.Location);
        }
        [Fact]
        public void WhenAnimateEntityMovesWithLocationPutsProcedureIntoUniverse()
        {
            var universe = new Mock<IUniverse>();
            universe.Setup(_ => _.Put(It.IsAny<IProcedure>()));
            var sut = GetAnimateEntity();
            sut.Universe = universe.Object;
            sut.SetLocation(NullLocation.Instance);

            sut.Perform(ProcedureType.Move, new List<IProcedureArg>());

            universe.Verify(_ => _.Put(It.IsAny<IProcedure>()), Times.Once);
        }

        [Fact]
        public void WhenAnimateEntityMovesWithoutLocationIgnores()
        {
            var universe = new Mock<IUniverse>();
            universe.Setup(_ => _.Put(It.IsAny<IProcedure>()));
            var sut = GetAnimateEntity();
            sut.Universe = universe.Object;

            sut.Perform(ProcedureType.Move, new List<IProcedureArg>());

            universe.Verify(_ => _.Put(It.IsAny<IProcedure>()), Times.Never);
        }

        [Fact]
        public void WhenAnimateEntityChangesLocationUpdatesMoveProcedure()
        {
            var sut = GetAnimateEntity();
            var location = GetLocation("fdsa");
            var location2 = GetLocation("zcxv");

            sut.SetLocation(location);
            sut.SetLocation(location2);

            Assert.Equal(location2, sut.Location);
        }

        private AnimateEntity GetAnimateEntity()
        {
            return new Mock<AnimateEntity>(
                DefaultMocks.GetProcedureBuilderMock().Object,
                DefaultMocks.GetTextDecorator().Object).Object;
        }

        private Location GetLocation(string Title)
        {
            return new Location(DefaultMocks.GetTextDecorator().Object) 
            { 
                Title = Title 
            };
        }
    }
}
