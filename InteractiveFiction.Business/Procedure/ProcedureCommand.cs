using InteractiveFiction.Business.Procedure.Argument;

namespace InteractiveFiction.Business.Procedure
{
    public class ProcedureCommand
    {
        public ProcedureType ProcedureType { get; set; }
        public List<IProcedureArg> Args { get; set; } = new List<IProcedureArg>();
    }
}
