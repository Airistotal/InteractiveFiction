using InteractiveFiction.Business.Entity;

namespace InteractiveFiction.Business.Procedure
{
    public class ProcedureBuilder : IProcedureBuilder
    {
        private ProcedureType procType;
        private IEntity? procAgent;
        private IEntity? procTarget;

        public IProcedureBuilder type(ProcedureType type)
        {
            this.procType = type;

            return this;
        }

        public IProcedureBuilder agent(IEntity agent)
        {
            this.procAgent = agent;

            return this;
        }

        public IProcedureBuilder target(IEntity target)
        {
            this.procTarget = target;

            return this;
        }

        public IProcedure build()
        {
            if (procAgent == null || procTarget == null)
            {
                throw new Exception("Unable to make procedure with both an agent and a target");
            }

            switch (this.procType)
            {
                case ProcedureType.Move:
                    return new MoveProcedure();
                default:
                    return new NullProcedure();
            }
        }
    }
}
