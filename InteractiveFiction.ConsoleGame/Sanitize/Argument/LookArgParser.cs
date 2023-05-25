using InteractiveFiction.Business.Existence;
using InteractiveFiction.Business.Procedure.Argument;

namespace InteractiveFiction.ConsoleGame.Sanitize.Argument
{
    public class LookArgParser : IArgParser
    {
        public List<IProcedureArg> Parse(List<string> args)
        {
            var results = new List<IProcedureArg>();

            if (args.Count == 0)
            {
                results.Add(new LookArg(Direction.NULL, ""));
            }
            else if (args.Count == 1)
            {
                var dir = Parse(args[0]);
                if (dir == Direction.NULL)
                {
                    results.Add(new LookArg(Direction.NULL, args[0]));
                } 
                else
                {
                    results.Add(new LookArg(dir, ""));
                }
            }
            else if (args.Count == 2)
            {
                results.Add(new LookArg(Parse(args[0]), args[1]));
            }

            return results;
        }

        private Direction Parse(string arg)
        {
            return arg?.ToUpper() switch
            {
                "N" or "NORTH" => Direction.North,
                "S" or "SOUTH" => Direction.South,
                "W" or "WEST" => Direction.West,
                "E" or "EAST" => Direction.East,
                _ => Direction.NULL
            };
        }
    }
}
