using InteractiveFiction.Business.Entity;
using InteractiveFiction.Business.Entity.Locations;
using InteractiveFiction.Business.Existence;
using InteractiveFiction.Business.Goal;
using InteractiveFiction.Business.Infrastructure;

namespace InteractiveFiction.Business.Procedure.Movement
{
    public class MoveProcedure : IProcedure, IObservable<IStat>
    {
        private readonly IAgent Agent;
        private readonly IEntity Entity;
        private Direction MoveDirection;
        private Location origin;
        private Location destination;
        private IList<IObserver<IStat>> observers = new List<IObserver<IStat>>();

        public MoveProcedure(IAgent agent)
        {
            if (agent is IEntity entity)
            {
                Agent = agent;
                Entity = entity;
                origin = Entity.GetLocation();
                destination = NullLocation.Instance;
            } else
            {
                throw new Exception("Unable to move agent that isn't an entity.");
            }
        }

        public IProcedure With(IList<IProcedureArg> args)
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

        public void Perform()
        {
            origin = Entity.GetLocation();
            destination = origin.Go(MoveDirection);
            CheckOrigin(origin);

            if (MoveDirection == Direction.NULL)
            {
                Agent.AddEvent($"Without a direction, you can't go anywhere. {origin.GetDirections()}");
                return;
            }

            if (destination.Type == LocationType.NULL)
            {
                AddNoMoveEvent();
                return;
            }

            destination.Children.Add(Entity);
            origin.Children.Remove(Entity);
            Entity.SetLocation(destination);
            AddMoveEvent(destination);

            SendToSubscribers();
        }

        private void SendToSubscribers()
        {
            foreach(var observer in observers)
            {
                observer.OnNext(GetAsStat());
            }
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

        public IStat GetAsStat()
        {
            return new MoveStat(destination.GetName());
        }

        public IDisposable Subscribe(IObserver<IStat> observer)
        {
            observers.Add(observer);

            return new DefaultObserverRemover(observers, observer);
        }
    }
}
