using InteractiveFiction.Business.Entity;
using InteractiveFiction.Business.Existence;
using InteractiveFiction.Business.Procedure;
using InteractiveFiction.Business.Procedure.Argument;
using InteractiveFiction.Business.Tests.Utils;
using Moq;

namespace InteractiveFiction.Business.Tests.Procedure
{
    public class MoveProcedureTests
    {
        [Fact]
        public void WhenMove_WithDestination_CanMove()
        {
            var harness = new MoveProcedureTestHarness()
                .WithDestination(Direction.North);

            harness.PerformMoveWithArg();

            harness.AssertMoved();
        }

        [Fact]
        public void WhenMove_WithoutDestination_CantMove()
        {
            var harness = new MoveProcedureTestHarness()
                .WithoutDestination(Direction.North);

            harness.PerformMoveWithArg();

            harness.AssertUnmoved();
        }

        [Fact]
        public void WhenMove_WithoutDirection_CantMove()
        {
            var harness = new MoveProcedureTestHarness()
                .WithDestination(Direction.NULL);

            harness.PerformMoveWithArg();

            harness.AssertUnmoved();
        }

        [Fact]
        public void WhenMoveAgentNotInLocationThrowsException()
        {
            var harness = new MoveProcedureTestHarness()
                .WithDestination(Direction.North)
                .WithoutAgent();

            Assert.Throws<ProcedureException>(() => harness.PerformMoveWithArg());
        }

        [Fact]
        public void WhenMove_WithoutOriginThrowsProcedureException()
        {
            var harness = new MoveProcedureTestHarness()
                .WithDestination(Direction.North)
                .WithoutAgent();

            Assert.Throws<ProcedureException>(() => harness.PerformMoveWithArg(false));
        }

        [Fact]
        public void WhenMove_WithNoMoveArg_ThrowsProcedureException()
        {
            var harness = new MoveProcedureTestHarness()
                .WithDestination(Direction.North)
                .WithoutAgent();

            Assert.Throws<ProcedureException>(() => harness.PerformMoveWithArg(false));
        }
    }
}
