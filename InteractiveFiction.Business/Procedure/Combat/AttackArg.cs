namespace InteractiveFiction.Business.Procedure.Combat
{
    public class AttackArg : IProcedureArg
    {
        public string TargetName { get; }

        public AttackArg(string targetName)
        {
            TargetName = targetName;
        }
    }
}
