using InteractiveFiction.Business.Entity;
using InteractiveFiction.Business.Procedure;
using Moq;

namespace InteractiveFiction.Business.Tests.Procedure
{
    public class ProcedureBuilderTests
    {
        [Fact]
        public void WhenBuildUnknownProcedureReturnsNullProcedure()
        {
            var procedureBuilder = new ProcedureBuilder();

            var procedure = procedureBuilder
                .type(ProcedureType.NULL)
                .agent(new Mock<IEntity>().Object)
                .target(new Mock<IEntity>().Object)
                .build();

            Assert.NotNull(procedure);
            Assert.IsType<NullProcedure>(procedure);
        }

        [Fact]
        public void WhenBuildMoveProcedureReturnsMoveProcedure()
        {
            var procedureBuilder = new ProcedureBuilder();

            var procedure = procedureBuilder
                .type(ProcedureType.Move)
                .agent(new Mock<IEntity>().Object)
                .target(new Mock<IEntity>().Object)
                .build();

            Assert.NotNull(procedure);
            Assert.IsType<MoveProcedure>(procedure);
        }

        [Fact]
        public void WhenBuildProcedureWithoutAgentThrowsException()
        {
            var procedureBuilder = new ProcedureBuilder();

            Assert.Throws<Exception>(() => procedureBuilder
                .type(ProcedureType.Move)
                .target(new Mock<IEntity>().Object)
                .build());
        }

        [Fact]
        public void WhenBuildProcedureWithoutTargetThrowsException()
        {
            var procedureBuilder = new ProcedureBuilder();

            Assert.Throws<Exception>(() => procedureBuilder
                .type(ProcedureType.Move)
                .agent(new Mock<IEntity>().Object)
                .build());
        }
    }
}
