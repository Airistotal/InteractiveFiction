using InteractiveFiction.Business.Procedure;
using InteractiveFiction.Business.Procedure.Combat;

namespace InteractiveFiction.ConsoleGame.Sanitize.Argument
{
    public class AttackArgParser : IArgParser
    {
        public List<IProcedureArg> Parse(List<string> args)
        {
            var results = new List<IProcedureArg>();

            if (args.Count == 0)
            {
                results.Add(new AttackArg(""));
            } else
            {
                results.Add(new AttackArg(args[0]));
            }

            return results;
        }
    }
}
