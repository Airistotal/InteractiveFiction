using InteractiveFiction.Business.Procedure;

namespace InteractiveFiction.ConsoleGame.Sanitize.Argument
{
    public class NullArgParser : IArgParser
    {
        public List<IProcedureArg> Parse(List<string> args)
        {
            return new List<IProcedureArg>();
        }
    }
}
