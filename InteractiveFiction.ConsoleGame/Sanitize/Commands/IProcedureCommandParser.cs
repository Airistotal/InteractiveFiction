using InteractiveFiction.Business.Procedure;

namespace InteractiveFiction.ConsoleGame.Sanitize.Commands
{
    public interface IProcedureCommandParser
    {
        ProcedureCommand Parse(string input);
    }
}
