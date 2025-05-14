using InteractiveFiction.ConsoleGame.Menu;

namespace InteractiveFiction.ConsoleGame.Sanitize.Commands
{
    public interface ICommandParser
    {
        Command Parse(string input);
    }
}
