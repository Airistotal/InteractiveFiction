using InteractiveFiction.Business.Existence;

namespace InteractiveFiction.Business.Procedure.Investigate
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
