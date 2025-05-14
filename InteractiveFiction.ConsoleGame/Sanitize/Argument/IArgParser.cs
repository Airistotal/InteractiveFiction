using InteractiveFiction.Business.Procedure.Argument;

namespace InteractiveFiction.ConsoleGame.Sanitize.Argument
{
    public interface IArgParser 
    { 
        List<IProcedureArg> Parse(List<string> args); 
    }
}