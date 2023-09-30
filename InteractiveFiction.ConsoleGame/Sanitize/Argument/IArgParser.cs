using InteractiveFiction.Business.Procedure;

namespace InteractiveFiction.ConsoleGame.Sanitize.Argument
{
    public interface IArgParser 
    { 
        List<IProcedureArg> Parse(List<string> args); 
    }
}