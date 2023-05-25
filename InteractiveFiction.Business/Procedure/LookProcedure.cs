using InteractiveFiction.Business.Entity;
using InteractiveFiction.Business.Existence;
using InteractiveFiction.Business.Procedure.Argument;

namespace InteractiveFiction.Business.Procedure
{
    public class LookProcedure : BaseProcedure, IProcedure
    {
        public LookProcedure(IEntity agent) : base(agent)
        {
        }

        public void Perform()
        {
            CheckAgentAndTarget();

            Agent.AddEvent(Target.GetFullDescription());
        }

        public IProcedure With(List<IProcedureArg> args)
        {
            CheckAgent();

            Target = Agent.GetLocation();

            if (args.Count > 0 && args[0] is LookArg lookArg)
            {

                if (lookArg.Direction == Direction.NULL && !string.IsNullOrWhiteSpace(lookArg.TargetName))
                {
                    Target = Agent.GetLocation().GetTarget(lookArg.TargetName);
                }
                else if (lookArg.Direction != Direction.NULL)
                {
                    if (string.IsNullOrWhiteSpace(lookArg.TargetName))
                    {
                        Target = Agent.GetLocation().Go(lookArg.Direction);
                    }
                    else
                    {
                        Target = Agent.GetLocation().Go(lookArg.Direction).GetTarget(lookArg.TargetName);

                    }
                }
            }

            return this;
        }
    }
}
