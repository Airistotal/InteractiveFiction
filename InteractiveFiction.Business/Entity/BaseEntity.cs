using InteractiveFiction.Business.Existence;
using InteractiveFiction.Business.Infrastructure;
using InteractiveFiction.Business.Procedure;
using InteractiveFiction.Business.Procedure.Argument;

namespace InteractiveFiction.Business.Entity
{
    public abstract class BaseEntity: IEntity
    {
        private readonly IProcedureBuilder procedureBuilder;
        protected readonly ITextDecorator textDecorator;
        private readonly List<string> NewEvents = new();

        protected Dictionary<ProcedureType, IProcedure> Capabilities { get; } = new();

        public IUniverse? Universe { get; set; }
        public List<IEntity> Children { get; } = new List<IEntity>();
        public Location Location { get; set; } = NullLocation.Instance;

        public BaseEntity(IProcedureBuilder procedureBuilder, ITextDecorator textDecorator)
        {
            this.procedureBuilder = procedureBuilder;
            this.textDecorator = textDecorator;
        }

        public void AddCapability(ProcedureType type)
        {
            if (procedureBuilder == null)
            {
                throw new Exception("Can't create procedure without a builder");
            }

            Capabilities.Add(type,
                procedureBuilder.type(type).agent(this).build());
        }

        public void Perform(ProcedureType type, List<IProcedureArg> args)
        {
            if (Universe == null)
            {
                throw new Exception("Can't perfom any procedure without a universe.");
            }

            if (Capabilities.ContainsKey(type))
            {
                Universe.Put(Capabilities[type].With(args));
            } else
            {
                AddEvent($"You can't {type.Name().ToLower()}!");
            }
        }

        public List<string> GetNewEvents()
        {
            return new List<string>(NewEvents);
        }

        public void ArchiveEvents()
        {
            NewEvents.Clear();
        }

        public void AddEvent(string evt)
        {
            NewEvents.Add(evt);
        }

        public void SetLocation(Location location)
        {
            Location = location;
        }

        public Location GetLocation()
        {
            return Location;
        }

        public void SetUniverse(IUniverse universe)
        {
            Universe = universe;
        }

        public abstract string GetFullDescription();

        public abstract bool Is(string id);
    }
}
