using InteractiveFiction.Business.Entity;
using InteractiveFiction.Business.Entity.Locations;
using InteractiveFiction.Business.Existence;
using InteractiveFiction.Business.Procedure;
using InteractiveFiction.Business.Procedure.Investigate;
using InteractiveFiction.Business.Tests.Utils;
using Moq;

namespace InteractiveFiction.Business.Tests.Procedure.Investigate
{
    public class LookProcedureTests
    {
        [Fact]
        public void WhenLookHere_NoArgs_LooksAtCurrentLocation()
        {
            var location = GetLocationWithConnections();
            var agent = GetAgent(location);
            var sut = new LookProcedure(agent.Object);

            sut.With(new List<IProcedureArg>() { }).Perform();

            agent.Verify(_ => _.AddEvent(location.GetFullDescription()), Times.Once);
        }

        [Fact]
        public void WhenLookHere_LooksAtCurrentLocation()
        {
            var location = GetLocationWithConnections();
            var agent = GetAgent(location);
            var sut = new LookProcedure(agent.Object);

            sut.With(new List<IProcedureArg>() { new LookArg(Direction.NULL, "") }).Perform();

            agent.Verify(_ => _.AddEvent(location.GetFullDescription()), Times.Once);
        }

        [Fact]
        public void WhenLookDirection_LooksAtThatLocation()
        {
            var location = GetLocationWithConnections();
            var agent = GetAgent(location);
            var sut = new LookProcedure(agent.Object);

            sut.With(new List<IProcedureArg>() { new LookArg(Direction.North, "") }).Perform();

            agent.Verify(_ => _.AddEvent(location.Go(Direction.North).GetFullDescription()), Times.Once);
        }

        [Fact]
        public void WhenLookAtTargetHere_LooksHereAtTarget()
        {
            var location = GetLocationWithConnections();
            var agent = GetAgent(location);
            var sut = new LookProcedure(agent.Object);

            sut.With(new List<IProcedureArg>() { new LookArg(Direction.NULL, "loc child") }).Perform();

            agent.Verify(_ => _.AddEvent("loc child"), Times.Once);
        }

        [Fact]
        public void WhenLookAtTargetNorth_LooksThereAtTarget()
        {
            var location = GetLocationWithConnections();
            var agent = GetAgent(location);
            var sut = new LookProcedure(agent.Object);

            sut.With(new List<IProcedureArg>() { new LookArg(Direction.North, "loc child 2") }).Perform();

            agent.Verify(_ => _.AddEvent("loc child 2"), Times.Once);
        }

        [Fact]
        public void WhenLook_WithoutTarget_ThrowsException()
        {
            var sut = new LookProcedure(new Mock<IEntity>().As<IAgent>().Object);

            Assert.Throws<ProcedureException>(() => sut.Perform());
        }

        [Fact]
        public void WhenLook_NotAsEntity_ThrowsException()
        {
            Assert.Throws<Exception>(() => new LookProcedure(new Mock<IAgent>().Object));
        }

        private Location GetLocationWithConnections()
        {
            var loc = GetLocation("title", "desc", "loc child");
            var loc2 = GetLocation("title2", "desc 2", "loc child 2");
            loc.AddPath(Direction.North, loc2);

            return loc;
        }

        private Location GetLocation(string title, string desc, string childDesc)
        {
            var loc = new Location(DefaultMocks.GetTextDecorator().Object)
            {
                Title = title,
                Description = desc
            };

            var locChild = new Mock<IEntity>();
            locChild.Setup(_ => _.Is(It.IsAny<string>())).Returns(true);
            locChild.Setup(_ => _.GetFullDescription()).Returns(childDesc);
            loc.Children.Add(locChild.Object);

            return loc;
        }

        public Mock<IAgent> GetAgent(Location location)
        {
            var entity = new Mock<IEntity>();
            entity.Setup(_ => _.GetLocation()).Returns(location);

            return entity.As<IAgent>();
        }
    }
}
