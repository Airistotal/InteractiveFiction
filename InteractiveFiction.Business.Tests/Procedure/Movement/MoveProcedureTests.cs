using InteractiveFiction.Business.Existence;

namespace InteractiveFiction.Business.Tests.Procedure.Movement
{
    public class MoveProcedureTests
    {
        [Fact]
        public void WhenMove_WithDestination_CanMove()
        {
            MoveProcedureFixture.GetFixture()
                .WithDestination(Direction.North)

                .PerformMoveWithArg()

                .AssertMoved();
        }

        [Fact]
        public void WhenMove_WithoutDestination_CantMove()
        {
            MoveProcedureFixture.GetFixture()
                .WithoutDestination(Direction.North)

                .PerformMoveWithArg()

                .AssertUnmoved();
        }

        [Fact]
        public void WhenMove_WithoutDirection_CantMove()
        {
            MoveProcedureFixture.GetFixture()
                .WithDestination(Direction.NULL)

                .PerformMoveWithArg()

                .AssertUnmoved();
        }

        [Fact]
        public void WhenMove_AgentNotInLocation_ThrowsException()
        {
            MoveProcedureFixture.GetFixture()
                .WithDestination(Direction.North)
                .WithoutAgent()

                .AssertMoveThrowsException();
        }

        [Fact]
        public void WhenMove_WithNoMoveArg_ThrowsProcedureException()
        {
            MoveProcedureFixture.GetFixture()
                .WithDestination(Direction.North)
                .WithoutAgent()

                .AssertMoveWithoutArgThrowsException();
        }

        [Fact]
        public void When_GetProcedureAsStat_ReturnsMoveStat()
        {
            MoveProcedureFixture.GetFixture()
                .WithDestination(Direction.North)
                .PerformMoveWithArg()

                .GetAsStat()

                .AssertStatIsMoveStat();
        }

        [Fact]
        public void When_ProcedureIsObserved_SendsMoveToObserver()
        {
            MoveProcedureFixture.GetFixture()
                .WithDestination(Direction.North)
                .WithObserver()

                .PerformMoveWithArg()

                .AssertObserverIsCalled();
        }
    }
}
