using InteractiveFiction.Business.Entity;
using InteractiveFiction.Business.Entity.Locations;
using InteractiveFiction.Business.Existence;
using InteractiveFiction.Business.Procedure.Argument;
using System.Diagnostics.CodeAnalysis;

namespace InteractiveFiction.Business.Procedure
{
    public class MoveProcedure : IProcedure
    {
        private readonly IEntity Agent;
        private readonly Location Origin;
        private Direction MoveDirection;

        public MoveProcedure(IEntity agent)
        {
            Agent = agent;
            Origin = agent.GetLocation();
        }

        public void Perform()
        {
            CheckOrigin();

            if (MoveDirection == Direction.NULL)
            {
                Agent.AddEvent($"Without a direction, you can't go anywhere. {Origin.GetDirections()}");
                return;
            }

            var destination = Origin.Go(MoveDirection);
            if (destination.Type == LocationType.NULL)
            {
                AddNoMoveEvent();
                return;
            }

            destination.Children.Add(Agent);
            Origin.Children.Remove(Agent);
            Agent.SetLocation(destination);
            AddMoveEvent(destination);
        }

        [MemberNotNull(nameof(Origin))]
        private void CheckOrigin()
        {
            if (Origin == null)
            {
                throw new ProcedureException("Unable to move if the target isn't a location.");
            }

            if (!Origin.Children.Contains(Agent))
            {
                throw new ProcedureException("Unable to move agent if it isn't in the right location.");
            }
        }

        private void AddMoveEvent(Location destination)
        {
            Agent.AddEvent($"You have moved {MoveDirection}.{Environment.NewLine}{Environment.NewLine}{destination.GetFullDescription()}");
        }

        private void AddNoMoveEvent()
        {
            CheckOrigin();

            Agent.AddEvent($"You can't go {MoveDirection}. {Origin.GetDirections()}");
        }

        public IProcedure With(List<IProcedureArg> args)
        {
            if (args != null && args.Count == 1 && args[0] is MoveArg mvArg)
            {
                MoveDirection = mvArg.Direction;
            } else
            {
                MoveDirection = Direction.NULL;
            }

            return this;
        }
    }
}
