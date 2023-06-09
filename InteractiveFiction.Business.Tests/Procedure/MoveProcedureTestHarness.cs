﻿using InteractiveFiction.Business.Entity;
using InteractiveFiction.Business.Entity.Locations;
using InteractiveFiction.Business.Existence;
using InteractiveFiction.Business.Procedure;
using InteractiveFiction.Business.Procedure.Argument;
using InteractiveFiction.Business.Tests.Utils;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace InteractiveFiction.Business.Tests.Procedure
{
    public class MoveProcedureTestHarness {
        private Mock<IEntity> Agent;
        private readonly Location Origin;
        private readonly Location Destination;
        private Direction? Direction;

        public MoveProcedureTestHarness()
        {
            Agent = new Mock<IEntity>();
            Origin = new Location(DefaultMocks.GetTextDecorator().Object);
            Destination = new Location(DefaultMocks.GetTextDecorator().Object)
            {
                Title = "Other Location",
                Description = "The other one"
            };

            Agent.Setup(_ => _.GetLocation()).Returns(Origin);
        }

        public MoveProcedureTestHarness WithDestination(Direction direction)
        {
            Origin.Children.Add(Agent.Object);
            Origin.AddPath(direction, Destination);
            Direction = direction;

            return this;
        }

        public MoveProcedureTestHarness WithoutDestination(Direction direction)
        {
            Origin.Children.Add(Agent.Object);
            Direction = direction;

            return this;
        }

        public MoveProcedureTestHarness WithoutAgent()
        {
            Origin.Children.Remove(Agent.Object);

            return this;
        }

        public void PerformMoveWithArg(bool withMvArg = true)
        {
            var sut = new MoveProcedure(Agent.Object);
            sut.With(new List<IProcedureArg>() { BuildArg(withMvArg) }).Perform();
        }

        private IProcedureArg BuildArg(bool withMvArg)
        {
            CheckMovement();

            if (withMvArg)
            {
                return new MoveArg(Direction.Value);
            } else
            {
                return new Mock<IProcedureArg>().Object;
            }
        }

        [MemberNotNull(nameof(Direction))]
        [MemberNotNull(nameof(Origin))]
        [MemberNotNull(nameof(Destination))]
        private void CheckMovement()
        {
            if (Direction == null || Origin == null || 
                Destination == null)
            {
                throw new Exception("Not enough info!");
            }
        }

        public void AssertMoved()
        {
            CheckMovement();

            Agent.Verify(_ => _.AddEvent(It.Is<string>(_ => _.Contains(Destination.GetFullDescription()))), Times.Once);
            Agent.Verify(_ => _.SetLocation(Destination), Times.Once);
            Assert.Contains(Agent.Object, Destination.Children);
            Assert.DoesNotContain(Agent.Object, Origin.Children);
        }

        public void AssertUnmoved()
        {
            Agent.Verify(_ => _.AddEvent(It.Is<string>(_ => _.Contains("can't go"))));
            Agent.Verify(_ => _.SetLocation(It.IsAny<Location>()), Times.Never);
            Assert.Contains(Agent.Object, Origin.Children);
        }
    }
}
