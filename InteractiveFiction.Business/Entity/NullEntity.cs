using InteractiveFiction.Business.Entity.Locations;
using InteractiveFiction.Business.Existence;
using InteractiveFiction.Business.Procedure;
using InteractiveFiction.Business.Procedure.Argument;

namespace InteractiveFiction.Business.Entity
{
    public class NullEntity : IEntity
    {
        private static NullEntity? _instance;

        public static NullEntity Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new NullEntity();
                }

                return _instance;
            }
        }

        private NullEntity() { }

        public void Perform(ProcedureType type, List<IProcedureArg> args) { }

        public void ArchiveEvents() { }

        public void AddEvent(string evt) { }

        public void SetLocation(Location location) { }

        public void SetUniverse(IUniverse universe) { }

        public bool Is(string id)
        {
            return false;
        }

        public Location GetLocation()
        {
            return NullLocation.Instance;
        }

        public List<string> GetNewEvents()
        {
            return new List<string>();
        }

        public string GetFullDescription()
        {
            return "This doesn't exist!";
        }

        public string GetName()
        {
            return "This doesn't exist!";
        }
    }
}
