using InteractiveFiction.Business.Entity;
using InteractiveFiction.Business.Procedure;
using InteractiveFiction.Business.Procedure.Combat;
using InteractiveFiction.Business.Procedure.Investigate;
using InteractiveFiction.Business.Procedure.Movement;
using Moq;

namespace InteractiveFiction.Business.Tests.Procedure
{
    public class ProcedureBuilderTests
    {
        [Theory]
        [InlineData(ProcedureType.NULL, typeof(NullProcedure))]
        [InlineData(ProcedureType.Move, typeof(MoveProcedure))]
        [InlineData(ProcedureType.Look, typeof(LookProcedure))]
        [InlineData(ProcedureType.Attack, typeof(AttackProcedure))]
        public void WhenBuildTypeReturnsProcedure(ProcedureType type, Type procedureClass)
        {
            var procedureBuilder = new ProcedureBuilder();

            var procedure = procedureBuilder
                .Type(type)
                .Agent(new Mock<IEntity>().As<IDamager>().As<IAgent>().Object)
                .Build();

            Assert.NotNull(procedure);
            Assert.IsType(procedureClass, procedure);
        }

        [Fact]
        public void WhenBuildProcedureWithoutAgentThrowsException()
        {
            var procedureBuilder = new ProcedureBuilder();

            Assert.Throws<ProcedureException>(() => procedureBuilder
                .Type(ProcedureType.Move)
                .Build());
        }
    }
}
