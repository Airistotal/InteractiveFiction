using InteractiveFiction.Business.Existence;

namespace InteractiveFiction.Business.Procedure.Movement
{
    public class MoveArg : IProcedureArg
    {
        public Direction Direction { get; }

        public MoveArg(Direction direction)
        {
            Direction = direction;
        }
    }
}
