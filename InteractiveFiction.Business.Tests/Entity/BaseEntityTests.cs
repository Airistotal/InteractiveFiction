using InteractiveFiction.Business.Entity;
using InteractiveFiction.Business.Existence;
using InteractiveFiction.Business.Procedure;
using InteractiveFiction.Business.Tests.Utils;
using Moq;

namespace InteractiveFiction.Business.Tests.Entity
{
    public class BaseEntityTests
    {
        [Fact]
        public void WhenUniverseEntityDoesProcedureWithoutUniverseThrowsException()
        {
            var sut = new Mock<BaseEntity>(DefaultMocks.GetProcedureBuilderMock().Object);

            Assert.Throws<Exception>(() => sut.Object.Perform(ProcedureType.Move, new[] { "" }));
        }

        [Fact]
        public void WhenUniverseEntityDoesProcedureNotInCapabilitiesIgnores()
        {
            var universe = new Mock<IUniverse>();
            universe.Setup(_ => _.Put(It.IsAny<IProcedure>()));
            var sut = new Mock<BaseEntity>(DefaultMocks.GetProcedureBuilderMock().Object);
            sut.Object.Universe = universe.Object;

            sut.Object.Perform(ProcedureType.Move, new[] { "" });

            universe.Verify(_ => _.Put(It.IsAny<IProcedure>()), Times.Never);
        }

        [Fact]
        public void WhenUniverseEntityDoesProcedureInCapabilitiesSendsToUniverse()
        {
            var universe = new Mock<IUniverse>();
            universe.Setup(_ => _.Put(It.IsAny<IProcedure>()));
            var sut = new Mock<BaseEntity>(DefaultMocks.GetProcedureBuilderMock().Object);
            sut.Object.Universe = universe.Object;
            sut.Object.AddCapability(ProcedureType.Move, new Mock<IEntity>().Object);
            sut.Object.Perform(ProcedureType.Move, new[] { "" });

            universe.Verify(_ => _.Put(It.IsAny<IProcedure>()), Times.Once);
        }

        [Fact]
        public void WhenAddCapabilityWithoutProcedureBuilderThrowsException()
        {
            var sut = new Mock<BaseEntity>(null);

            Assert.Throws<Exception>(() => sut.Object.AddCapability(ProcedureType.Move, new Mock<IEntity>().Object));
        }
    }
}
