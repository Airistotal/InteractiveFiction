using InteractiveFiction.Business.Entity;
using InteractiveFiction.Business.Existence;
using InteractiveFiction.Business.Procedure;
using InteractiveFiction.Business.Procedure.Argument;
using InteractiveFiction.Business.Tests.Utils;
using Moq;

namespace InteractiveFiction.Business.Tests.Entity
{
    public class BaseEntityTests
    {
        [Fact]
        public void WhenUniverseEntityDoesProcedureWithoutUniverseThrowsException()
        {
            var sut = GetBaseEntity();

            Assert.Throws<Exception>(() => sut.Perform(ProcedureType.Move, new List<IProcedureArg>()));
        }

        [Fact]
        public void WhenUniverseEntityDoesProcedureNotInCapabilitiesIgnores()
        {
            var universe = new Mock<IUniverse>();
            universe.Setup(_ => _.Put(It.IsAny<IProcedure>()));
            var sut = GetBaseEntity();
            sut.Universe = universe.Object;

            sut.Perform(ProcedureType.Move, new List<IProcedureArg>());

            universe.Verify(_ => _.Put(It.IsAny<IProcedure>()), Times.Never);
        }

        [Fact]
        public void WhenUniverseEntityDoesProcedureInCapabilitiesSendsToUniverse()
        {
            var universe = new Mock<IUniverse>();
            universe.Setup(_ => _.Put(It.IsAny<IProcedure>()));
            var sut = GetBaseEntity();
            sut.Universe = universe.Object;
            sut.AddCapability(ProcedureType.Move);
            sut.Perform(ProcedureType.Move, new List<IProcedureArg>());

            universe.Verify(_ => _.Put(It.IsAny<IProcedure>()), Times.Once);
        }

        [Fact]
        public void WhenAddCapabilityWithoutProcedureBuilderThrowsException()
        {
            var sut = new Mock<BaseEntity>(null, null);

            Assert.Throws<Exception>(() => sut.Object.AddCapability(ProcedureType.Move));
        }

        [Fact]
        public void WhenAddEvent_AddsToNewEvents()
        {
            var evt = "text";
            var sut = GetBaseEntity();

            sut.AddEvent(evt);
            var events = sut.GetNewEvents();

            Assert.Contains(evt, events);
        }

        [Fact]
        public void WhenArchiveEvents_RemovesEvents()
        {
            var evt = "text";
            var sut = GetBaseEntity();

            sut.AddEvent(evt);
            sut.ArchiveEvents();

            Assert.Empty(sut.GetNewEvents());
        }

        [Fact]
        public void WhenPerformProcedure_NotFound_AddsEvent()
        {
            var sut = GetBaseEntity();
            sut.Universe = new Mock<IUniverse>().Object;

            sut.Perform(ProcedureType.Move, new List<IProcedureArg>());

            Assert.Contains(sut.GetNewEvents(), _ => _.Contains("You can't"));
        }

        private BaseEntity GetBaseEntity()
        {
            return new Mock<BaseEntity>(
                DefaultMocks.GetProcedureBuilderMock().Object,
                DefaultMocks.GetTextDecorator().Object).Object;
        }
    }
}
