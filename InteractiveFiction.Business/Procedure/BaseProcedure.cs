using InteractiveFiction.Business.Entity;
using System.Diagnostics.CodeAnalysis;

namespace InteractiveFiction.Business.Procedure
{
    public abstract class BaseProcedure
    {
        public IEntity Agent { get; set; }
        public IEntity? Target { get; set; }

        public BaseProcedure(IEntity agent)
        {
            Agent = agent;
        }

        [MemberNotNull(nameof(Agent))]
        [MemberNotNull(nameof(Target))]
        protected void CheckAgentAndTarget()
        {
            if (Agent == null || Target == null)
            {
                throw new ProcedureException("Unable to proceed without agent or target.");
            }
        }

        [MemberNotNull(nameof(Agent))]
        protected void CheckAgent()
        {
            if (Agent == null)
            {
                throw new ProcedureException("Unable to proceed without agent.");
            }
        }
    }
}
