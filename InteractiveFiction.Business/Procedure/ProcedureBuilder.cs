using InteractiveFiction.Business.Entity;
using System.Diagnostics.CodeAnalysis;

namespace InteractiveFiction.Business.Procedure
{
    public class ProcedureBuilder : IProcedureBuilder
    {
        private ProcedureType Type;
        private IEntity? Agent;

        public IProcedureBuilder type(ProcedureType type)
        {
            Type = type;

            return this;
        }

        public IProcedureBuilder agent(IEntity agent)
        {
            Agent = agent;

            return this;
        }

        public IProcedure build()
        {
            CheckAgent();

            return Type switch
            {
                ProcedureType.Look => new LookProcedure(Agent),
                ProcedureType.Move => new MoveProcedure(Agent),
                _ => new NullProcedure(),
            };
        }

        [MemberNotNull(nameof(Agent))]
        private void CheckAgent()
        {
            if (Agent == null)
            {
                throw new ProcedureException("Unable to build procedure without agent");
            }
        }
    }
}
