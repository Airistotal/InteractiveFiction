using InteractiveFiction.Business.Entity;
using InteractiveFiction.Business.Exceptions;
using InteractiveFiction.Business.Existence;
using InteractiveFiction.Business.Procedure;
using InteractiveFiction.Business.Tests.Utils;
using Moq;

namespace InteractiveFiction.Business.Tests.Procedure
{
    public class MoveProcedureTests
    {
        [Fact]
        public void WhenMoveToExistingLocationGoesToOtherLocation()
        {
            var move = GetMoveProcedure();

            move.With(new[] { Direction.North }).Perform();

            Assert.NotNull(move.Target);
            Assert.NotNull(move.Agent);

            if (move.Target != null && move.Agent != null)
            {
                Location oldLocation = (Location)move.Target;
                Location newLocation = oldLocation.Go(Direction.North);
                AnimateEntity agent = (AnimateEntity)move.Agent;
                Assert.DoesNotContain(move.Agent, oldLocation.Children);
                Assert.Contains(move.Agent, newLocation.Children);
                Assert.Equal(newLocation, agent.Location);
            }
        }

        [Fact]
        public void WhenMoveToNullLocationCantGo()
        {
            var move = GetMoveProcedure();
            var target = move.Target as Location;
            target?.DestroyPath(Direction.North);

            move.With(new[] { Direction.North }).Perform();

            Assert.Contains(move.Agent, target?.Children);
        }

        [Fact]
        public void WhenMoveNotInLocationThrowsException()
        {
            var move = GetMoveProcedure();
            var target = move.Target as Location;
            if (move.Agent != null)
            {
                target?.Children.Remove(move.Agent);
            }

            Assert.Throws<MoveException>(() => move.With(new[] { Direction.North }).Perform());
        }

        [Fact]
        public void WhenMoveTargetIsntLocationThrowsException()
        {
            var move = GetMoveProcedure();
            move.Target = new Mock<IEntity>().Object;

            Assert.Throws<MoveException>(() => move.With(new[] { Direction.North }).Perform());
        }

        [Fact]
        public void WhenMoveAgentIsNullThrowsException()
        {
            var move = GetMoveProcedure();
            move.Agent = null;

            Assert.Throws<NullAgentException>(() => move.With(new[] { Direction.North }).Perform());
        }

        [Fact]
        public void WhenMoveArgIsntDirectionThrowsException()
        {
            var move = GetMoveProcedure();

            Assert.Throws<ArgumentException>(() => move.With(new[] { "" }).Perform());
        }

        private static MoveProcedure GetMoveProcedure()
        {
            var location = new Location(DefaultMocks.GetProcedureBuilderMock().Object)
            {
                Title = "Location"
            };
            var otherLocation = new Location(DefaultMocks.GetProcedureBuilderMock().Object)
            {
                Title = "Other Location"
            };

            location.AddPath(Direction.North, otherLocation);
            var entity = new TestAnimateEntity();
            location.Children.Add(entity);

            return new MoveProcedure()
            {
                Agent = entity,
                Target = location,
            };
        }
    } 
}
