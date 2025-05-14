using Moq;
using InteractiveFiction.Business.Existence;
using InteractiveFiction.Business.Procedure;
using InteractiveFiction.Business.Entity;

namespace InteractiveFiction.Business.Tests.Existence
{
    public class UniverseTests
    {
        [Fact]
        public void WhenTickHappensLawApplies()
        {
            var law = new Mock<ILaw>();
            law.Setup(_ => _.apply());
            var universe = new Universe(new Mock<IEntity>().Object);
            universe.RegisterLaw(law.Object);

            universe.Tick();

            law.Verify(_ => _.apply(), Times.Once);
        }

        [Fact]
        public void WhenTickHappensExecutesActions()
        {
            var procedure = new Mock<IProcedure>();
            procedure.Setup(_ => _.Perform());
            var universe = new Universe(new Mock<IEntity>().Object);
            universe.Put(procedure.Object);

            universe.Tick();

            procedure.Verify(_ => _.Perform(), Times.Once);
        }
    }
}