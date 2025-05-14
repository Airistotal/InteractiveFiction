using InteractiveFiction.Business.Procedure.Argument;

namespace InteractiveFiction.Business.Procedure
{
    public interface IProcedure
    {
        IProcedure With(List<IProcedureArg> args);
        void Perform();
    }
}
