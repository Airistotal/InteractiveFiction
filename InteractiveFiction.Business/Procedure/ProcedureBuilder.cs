using InteractiveFiction.Business.Entity;
using System.Diagnostics.CodeAnalysis;
using InteractiveFiction.Business.Procedure.Combat;
using InteractiveFiction.Business.Procedure.Investigate;
using InteractiveFiction.Business.Procedure.Movement;

namespace InteractiveFiction.Business.Procedure
{
    public class ProcedureBuilder : IProcedureBuilder
    {
        private ProcedureType BuildType;
        private IAgent? BuildAgent;

        public IProcedureBuilder Type(ProcedureType type)
        {
            BuildType = type;

            return this;
        }

        public IProcedureBuilder Agent(IAgent agent)
        {
            BuildAgent = agent;

            return this;
        }

        public IProcedure Build()
        {
            CheckAgent();

            return BuildType switch
            {
                ProcedureType.Look => new LookProcedure(BuildAgent),
                ProcedureType.Move => new MoveProcedure(BuildAgent),
                ProcedureType.Attack => new AttackProcedure(BuildAgent),
                _ => new NullProcedure(),
            };
        }

        [MemberNotNull(nameof(BuildAgent))]
        private void CheckAgent()
        {
            if (BuildAgent == null)
            {
                throw new ProcedureException("Unable to build procedure without agent");
            }
        }
    }
}
