using InteractiveFiction.Business.Existence;
using InteractiveFiction.Business.Procedure;

namespace InteractiveFiction.Business.Entity
{
    public abstract class BaseEntity: IEntity
    {
        private readonly IProcedureBuilder? procedureBuilder;

        protected Dictionary<ProcedureType, IProcedure> Capabilities { get; } = new();

        public IUniverse? Universe { get; set; }
        public List<IEntity> Children { get; } = new List<IEntity>();
        public IEntity? Location { get; set; }


        public BaseEntity(IProcedureBuilder? procedureBuilder)
        {
            this.procedureBuilder = procedureBuilder;
        }

        public void AddCapability(ProcedureType type, IEntity target)
        {
            if (procedureBuilder == null)
            {
                throw new Exception("Can't create procedure without a builder");
            }

            Capabilities.Add(type,
                procedureBuilder.type(type).agent(this).target(target).build());
        }

        public void Perform(ProcedureType type, string[] args)
        {
            if (Universe == null)
            {
                throw new Exception("Can't perfom any procedure without a universe.");
            }

            if (Capabilities.ContainsKey(type))
            {
                Universe.Put(Capabilities[type].With(args));
            }
        }
    }
}
