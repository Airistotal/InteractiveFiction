using InteractiveFiction.Business.Goal;

namespace InteractiveFiction.Business.Procedure
{
    public class NullProcedure : IProcedure
    {
        public IStat GetAsStat()
        {
            throw new NotImplementedException();
        }

        public void Perform() { }

        public IProcedure With(IList<IProcedureArg> args) { return this; }
    }
}
