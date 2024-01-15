using InteractiveFiction.Business.Goal;

namespace InteractiveFiction.Business.Procedure
{
    public interface IProcedure
    {
        IProcedure With(IList<IProcedureArg> args);
        IStat GetAsStat();
        void Perform();
    }
}
