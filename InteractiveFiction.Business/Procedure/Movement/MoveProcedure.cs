using InteractiveFiction.Business.Entity;
using InteractiveFiction.Business.Entity.Locations;
using InteractiveFiction.Business.Existence;
using InteractiveFiction.Business.Goal;

namespace InteractiveFiction.Business.Procedure.Movement
{
    public class MoveProcedure : IProcedure
    {
        private readonly IAgent Agent;
        private readonly IEntity Entity;
        private Direction MoveDirection;

        public MoveProcedure(IAgent agent)
        {
            if (agent is IEntity entity)
            {
                Agent = agent;
                Entity = entity;

            } else
            {
                throw new Exception("Unable to move agent that isn't an entity.");
            }
        }

        public void Perform()
        {
            var origin = Entity.GetLocation();
            CheckOrigin(origin);

            if (MoveDirection == Direction.NULL)
            {
                Agent.AddEvent($"Without a direction, you can't go anywhere. {origin.GetDirections()}");
                return;
            }

            var destination = origin.Go(MoveDirection);
            if (destination.Type == LocationType.NULL)
            {
                AddNoMoveEvent();
                return;
            }

            destination.Children.Add(Entity);
            origin.Children.Remove(Entity);
            Entity.SetLocation(destination);
            AddMoveEvent(destination);
        }

        private void CheckOrigin(Location Origin)
        {
            if (Origin == null)
            {
                throw new ProcedureException("Unable to move if the target isn't a location.");
            }

            if (!Origin.Children.Contains(Entity))
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
            var origin = Entity.GetLocation();
            Agent.AddEvent($"You can't go {MoveDirection}. {origin.GetDirections()}");
        }

        public IProcedure With(List<IProcedureArg> args)
        {
            if (args != null && args.Count == 1 && args[0] is MoveArg mvArg)
            {
                MoveDirection = mvArg.Direction;
            }
            else
            {
                MoveDirection = Direction.NULL;
            }

            return this;
        }

        public IStat GetAsStat()
        {
            throw new NotImplementedException();
        }
    }
}
