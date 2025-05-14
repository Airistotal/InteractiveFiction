using InteractiveFiction.Business.Procedure;
using InteractiveFiction.ConsoleGame.Sanitize.Argument;

namespace InteractiveFiction.ConsoleGame.Sanitize.Commands
{
    public class ProcedureCommandParser : IProcedureCommandParser
    {
        private readonly IArgParserFactory argParserFactory;

        public ProcedureCommandParser(IArgParserFactory argParserFactory)
        {
            this.argParserFactory = argParserFactory;
        }

        public ProcedureCommand Parse(string input)
        {
            var parts = input.Split(" ");
            var result = new ProcedureCommand
            {
                ProcedureType = ProcedureType.NULL
            };

            if (parts.Length > 0)
            {
                result.ProcedureType = ParseProcedureType(parts[0]);
            }

            if (parts.Length > 1)
            {
                var parser = argParserFactory.Create(result.ProcedureType);
                result.Args = parser.Parse(parts.Skip(1).ToList());
            }

            return result;
        }

        private static ProcedureType ParseProcedureType(string input)
        {
            return input.ToLower() switch
            {
                "l" or "look" => ProcedureType.Look,
                "m" or "move" => ProcedureType.Move,
                "atk" or "attack" => ProcedureType.Attack,
                "save" => ProcedureType.Save,
                _ => ProcedureType.NULL,
            };
        }
    }
}
