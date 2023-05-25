using InteractiveFiction.Business.Entity;
using InteractiveFiction.Business.Procedure;
using Moq;

namespace InteractiveFiction.Business.Tests.Procedure
{
    public class ProcedureBuilderTests
    {
        [Theory]
        [InlineData(ProcedureType.NULL, typeof(NullProcedure))]
        [InlineData(ProcedureType.Move, typeof(MoveProcedure))]
        [InlineData(ProcedureType.Look, typeof(LookProcedure))]
        public void WhenBuildTypeReturnsProcedure(ProcedureType type, Type procedureClass)
        {
            var procedureBuilder = new ProcedureBuilder();

            var procedure = procedureBuilder
                .type(type)
                .agent(new Mock<IEntity>().Object)
                .build();

            Assert.NotNull(procedure);
            Assert.IsType(procedureClass, procedure);
        }

        [Fact]
        public void WhenBuildProcedureWithoutAgentThrowsException()
        {
            var procedureBuilder = new ProcedureBuilder();

            Assert.Throws<ProcedureException>(() => procedureBuilder
                .type(ProcedureType.Move)
                .build());
        }
    }
}
