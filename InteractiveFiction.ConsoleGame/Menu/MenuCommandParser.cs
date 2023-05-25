using InteractiveFiction.ConsoleGame.Sanitize.Commands;

namespace InteractiveFiction.ConsoleGame.Menu
{
    public class MenuCommandParser : ICommandParser
    {
        public Command Parse(string input)
        {
            return input switch
            {
                "boot" => Command.BOOT,
                "1" => Command.ACTION1,
                _ => Command.NULL,
            };
        }
    }
}
