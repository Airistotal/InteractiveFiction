using InteractiveFiction.Business.Entity;
using InteractiveFiction.Business.Entity.Locations;
using InteractiveFiction.Business.Existence;
using InteractiveFiction.Business.Procedure;
using InteractiveFiction.Business.Tests.Utils;
using Moq;

namespace InteractiveFiction.Business.Tests.Entity
{
    public class BaseAgentTests
    {
        [Fact]
        public void WhenUniverseEntityDoesProcedureWithoutUniverseThrowsException()
        {
            var sut = GetAgent();

            Assert.Throws<Exception>(() => sut.Perform(ProcedureType.Move, new List<IProcedureArg>()));
        }

        [Fact]
        public void WhenUniverseEntityDoesProcedureNotInCapabilitiesIgnores()
        {
            var universe = new Mock<IUniverse>();
            universe.Setup(_ => _.Put(It.IsAny<IProcedure>()));
            var sut = GetAgent();
            sut.Universe = universe.Object;

            sut.Perform(ProcedureType.Move, new List<IProcedureArg>());

            universe.Verify(_ => _.Put(It.IsAny<IProcedure>()), Times.Never);
        }

        [Fact]
        public void WhenUniverseEntityDoesProcedureInCapabilitiesSendsToUniverse()
        {
            var universe = new Mock<IUniverse>();
            universe.Setup(_ => _.Put(It.IsAny<IProcedure>()));
            var sut = GetAgent();
            sut.Universe = universe.Object;
            sut.AddCapability(ProcedureType.Move);
            sut.Perform(ProcedureType.Move, new List<IProcedureArg>());

            universe.Verify(_ => _.Put(It.IsAny<IProcedure>()), Times.Once);
        }

        [Fact]
        public void WhenAddCapabilityWithoutProcedureBuilderThrowsException()
        {
            var sut = new Mock<BaseAgent>(null);

            Assert.Throws<Exception>(() => sut.Object.AddCapability(ProcedureType.Move));
        }

        [Fact]
        public void WhenAddEvent_AddsToNewEvents()
        {
            var evt = "text";
            var sut = GetAgent();

            sut.AddEvent(evt);
            var events = sut.GetNewEvents();

            Assert.Contains(evt, events);
        }

        [Fact]
        public void WhenArchiveEvents_RemovesEvents()
        {
            var evt = "text";
            var sut = GetAgent();

            sut.AddEvent(evt);
            sut.ArchiveEvents();

            Assert.Empty(sut.GetNewEvents());
        }

        [Fact]
        public void WhenPerformProcedure_NotFound_AddsEvent()
        {
            var sut = GetAgent();
            sut.Universe = new Mock<IUniverse>().Object;

            sut.Perform(ProcedureType.Move, new List<IProcedureArg>());

            Assert.Contains(sut.GetNewEvents(), _ => _.Contains("You can't"));
        }

        [Fact]
        public void WhenAgentChangesLocation_UpdatesLocation()
        {
            var sut = GetAgent();
            var location = GetLocation("fdsa");
            var location2 = GetLocation("zcxv");

            sut.SetLocation(location);
            sut.SetLocation(location2);

            Assert.Equal(location2, sut.Location);
        }

        private static BaseAgent GetAgent()
        {
            return new Mock<BaseAgent>(DefaultMocks.GetProcedureBuilderMock().Object).Object;
        }

        private static Location GetLocation(string Title)
        {
            return new Location(DefaultMocks.GetTextDecorator().Object)
            {
                Title = Title
            };
        }
    }
}
