using InteractiveFiction.Business.Entity;
using InteractiveFiction.Business.Existence;
using InteractiveFiction.Business.Goal;
using System.Diagnostics.CodeAnalysis;

namespace InteractiveFiction.Business.Procedure.Investigate
{
    public class LookProcedure : IProcedure
    {
        public IAgent Agent { get; set; }
        public IEntity Entity { get; set; }
        public IEntity? Target { get; set; }

        public LookProcedure(IAgent agent)
        {
            if (agent is IEntity entity)
            {
                this.Agent = agent;
                this.Entity = entity;
            } else
            {
                throw new Exception("Unable to look with an agent that isn't an entity.");
            }
        }

        public void Perform()
        {
            CheckAgentAndTarget();

            Agent.AddEvent(Target.GetFullDescription());
        }

        public IProcedure With(List<IProcedureArg> args)
        {
            CheckAgent();

            Target = Entity.GetLocation();

            if (args.Count > 0 && args[0] is LookArg lookArg)
            {

                if (lookArg.Direction == Direction.NULL && !string.IsNullOrWhiteSpace(lookArg.TargetName))
                {
                    Target = Entity.GetLocation().GetTarget(lookArg.TargetName);
                }
                else if (lookArg.Direction != Direction.NULL)
                {
                    if (string.IsNullOrWhiteSpace(lookArg.TargetName))
                    {
                        Target = Entity.GetLocation().Go(lookArg.Direction);
                    }
                    else
                    {
                        Target = Entity.GetLocation().Go(lookArg.Direction).GetTarget(lookArg.TargetName);

                    }
                }
            }

            return this;
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

        public IStat GetAsStat()
        {
            throw new NotImplementedException();
        }
    }
}
