using InteractiveFiction.Business.Procedure.Argument;

namespace InteractiveFiction.Business.Procedure
{
    public class NullProcedure : IProcedure
    {
        public void Perform() { }

        public IProcedure With(List<IProcedureArg> args) { return this; }
    }
}
