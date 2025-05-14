using InteractiveFiction.Business.Existence;

namespace InteractiveFiction.Business.Procedure.Argument
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
