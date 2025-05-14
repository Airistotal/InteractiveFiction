using InteractiveFiction.Business.Entity;
using InteractiveFiction.Business.Existence;
using InteractiveFiction.Business.Procedure.Argument;

namespace InteractiveFiction.ConsoleGame.Sanitize.Argument
{
    public class MoveArgParser : IArgParser

    {
        public List<IProcedureArg> Parse(List<string> args)
        {
            var results = new List<IProcedureArg>();

            foreach (var arg in args)
            {
                results.Add(Parse(arg));
            }

            return results;
        }

        private static IProcedureArg Parse(string arg)
        {
            return arg?.ToLower() switch
            {
                "n" or "north" => new MoveArg(Direction.North),
                "s" or "south" => new MoveArg(Direction.South),
                "w" or "west" => new MoveArg(Direction.West),
                "e" or "east" => new MoveArg(Direction.East),
                "i" or "in" => new MoveArg(Direction.In),
                "o" or "out" => new MoveArg(Direction.Out),
                _ => new MoveArg(Direction.NULL)
            };
        }
    }
}