using InteractiveFiction.Business.Existence;

namespace InteractiveFiction.Business.Procedure.Argument
{
    public class LookArg : IProcedureArg
    {
        public Direction Direction { get; }
        public string TargetName { get; }

        public LookArg(Direction direction, string targetName)
        {
            Direction = direction;
            TargetName = targetName;
        }
    }
}
