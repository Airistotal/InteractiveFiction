using InteractiveFiction.Business.Procedure;

namespace InteractiveFiction.ConsoleGame.Sanitize.Argument
{
    public class ArgParserFactory : IArgParserFactory
    {
        public IArgParser Create(ProcedureType type) => type switch
        {
            ProcedureType.Look => new LookArgParser(),
            ProcedureType.Move => new MoveArgParser(),
            _ => new NullArgParser(),
        };
    }
}
