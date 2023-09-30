using InteractiveFiction.Business.Entity.Locations;
using InteractiveFiction.Business.Existence;
using InteractiveFiction.Business.Procedure;

namespace InteractiveFiction.Business.Entity
{
    public abstract class BaseAgent : IAgent
    {
        private readonly IProcedureBuilder procedureBuilder;

        private readonly List<string> NewEvents = new();
        public IUniverse? Universe { get; set; }
        protected Dictionary<ProcedureType, IProcedure> Capabilities { get; } = new();
        public Location Location { get; set; } = NullLocation.Instance;

        public BaseAgent(IProcedureBuilder procedureBuilder)
        {
            this.procedureBuilder = procedureBuilder;
        }

        public void AddCapability(ProcedureType type)
        {
            if (procedureBuilder == null)
            {
                throw new Exception("Can't create procedure without a builder");
            }

            Capabilities.Add(type,
                procedureBuilder.Type(type).Agent(this).Build());
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
            }
            else
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
    }
}
