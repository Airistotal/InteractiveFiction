using InteractiveFiction.Business.Entity;
using InteractiveFiction.Business.Entity.Locations;
using InteractiveFiction.Business.Existence;
using InteractiveFiction.Business.Goal;
using InteractiveFiction.Business.Procedure;
using InteractiveFiction.Business.Procedure.Movement;
using InteractiveFiction.Business.Tests.Utils;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace InteractiveFiction.Business.Tests.Procedure.Movement
{
    public class MoveProcedureFixture
    {
        private IStat stat;
        private Mock<IAgent> Agent;
        private Mock<IEntity> Entity;
        private readonly Location Origin;
        private readonly Location Destination;
        private Direction? Direction;
        private MoveProcedure? sut;
        private Mock<IObserver<IStat>> observer;

        private MoveProcedureFixture()
        {
            Agent = new Mock<IAgent>();
            Entity = Agent.As<IEntity>();
            Origin = new Location(DefaultMocks.GetTextDecorator().Object);
            Destination = new Location(DefaultMocks.GetTextDecorator().Object)
            {
                Title = "Other Location",
                Description = "The other one"
            };

            Entity.Setup(_ => _.GetLocation()).Returns(Origin);
        }

        public static MoveProcedureFixture GetFixture()
        {
            return new MoveProcedureFixture();
        }

        public MoveProcedureFixture WithObserver()
        {
            observer = new Mock<IObserver<IStat>>();

            return this;
        }

        public MoveProcedureFixture WithDestination(Direction direction)
        {
            Origin.Children.Add(Entity.Object);
            Origin.AddPath(direction, Destination);
            Direction = direction;

            return this;
        }

        public MoveProcedureFixture WithoutDestination(Direction direction)
        {
            Origin.Children.Add(Entity.Object);
            Direction = direction;

            return this;
        }

        public MoveProcedureFixture WithoutAgent()
        {
            Origin.Children.Remove(Entity.Object);

            return this;
        }

        public MoveProcedureFixture PerformMoveWithArg(bool withMvArg = true)
        {
            sut = new MoveProcedure(Agent.Object);
            if (observer != null)
            {
                sut.Subscribe(observer.Object);
            }

            sut.With(new List<IProcedureArg>() { BuildArg(withMvArg) }).Perform();

            return this;
        }

        public MoveProcedureFixture GetAsStat()
        {
            sut ??= new MoveProcedure(Agent.Object);
            stat = sut.GetAsStat();

            return this;
        }

        private IProcedureArg BuildArg(bool withMvArg)
        {
            CheckMovement();

            if (withMvArg)
            {
                return new MoveArg(Direction.Value);
            }
            else
            {
                return new Mock<IProcedureArg>().Object;
            }
        }

        [MemberNotNull(nameof(Direction))]
        [MemberNotNull(nameof(Origin))]
        [MemberNotNull(nameof(Destination))]
        private void CheckMovement()
        {
            if (Direction == null || Origin == null || Destination == null)
            {
                throw new Exception("Not enough info!");
            }
        }

        public void AssertMoved()
        {
            CheckMovement();

            Agent.Verify(_ => _.AddEvent(It.Is<string>(_ => _.Contains(Destination.GetFullDescription()))), Times.Once);
            Entity.Verify(_ => _.SetLocation(Destination), Times.Once);
            Assert.Contains(Entity.Object, Destination.Children);
            Assert.DoesNotContain(Entity.Object, Origin.Children);
        }

        public void AssertUnmoved()
        {
            Agent.Verify(_ => _.AddEvent(It.Is<string>(_ => _.Contains("can't go"))));
            Entity.Verify(_ => _.SetLocation(It.IsAny<Location>()), Times.Never);
            Assert.Contains(Entity.Object, Origin.Children);
        }

        public void AssertMoveThrowsException()
        {
            Assert.Throws<ProcedureException>(() => PerformMoveWithArg());
        }

        public void AssertMoveWithoutArgThrowsException()
        {
            Assert.Throws<ProcedureException>(() => PerformMoveWithArg(false));
        }

        public void AssertStatIsMoveStat()
        {
            if (stat == null)
            {
                Assert.False(true);
            }

            Assert.IsType<MoveStat>(stat);
        }

        public void AssertGetAsStatThrowsException()
        {
            Assert.Throws<ProcedureNotPerformedException>(() => PerformMoveWithArg());
        }

        public void AssertObserverIsCalled()
        {
            observer.Verify(x => x.OnNext(It.IsAny<IStat>()), Times.Once);
        }
    }
}
