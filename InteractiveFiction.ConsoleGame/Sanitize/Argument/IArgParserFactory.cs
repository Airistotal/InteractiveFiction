using InteractiveFiction.Business.Procedure;

namespace InteractiveFiction.ConsoleGame.Sanitize.Argument
{
    public interface IArgParserFactory
    {
        IArgParser Create(ProcedureType type);
    }
}