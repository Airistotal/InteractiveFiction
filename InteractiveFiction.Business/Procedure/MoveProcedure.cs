using InteractiveFiction.Business.Entity;
using InteractiveFiction.Business.Exceptions;
using InteractiveFiction.Business.Existence;

namespace InteractiveFiction.Business.Procedure
{
    public class MoveProcedure : BaseProcedure, IProcedure
    {
        public Direction Direction { get; set; }

        public void Perform()
        {
            if (Agent == null)
            {
                throw new NullAgentException("Can't move if nothing is moving.");
            }

            if (Target is Location location && Agent is BaseEntity baseAgent)
            {
                if (!location.Children.Contains(Agent))
                {
                    throw new MoveException("Unable to move agent if it isn't in the right location.");
                }

                var destination = location.Go(Direction);
                if (destination.Type == LocationType.NULL)
                {
                    return;
                }

                destination.Children.Add(Agent);
                location.Children.Remove(Agent);
                baseAgent.Location = destination;
            } else
            {
                throw new MoveException("Can't move if the target isn't a location.");
            }
        }

        public IProcedure With<T>(T[] args)
        {
            if (args != null && args.Length == 1 && args[0] is Direction direction)
            {
                Direction = direction;

                return this;
            } else
            {
                throw new ArgumentException("Unable to move without direction");
            }
        }
    }
}
