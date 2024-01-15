using InteractiveFiction.Business.Entity.Locations;
using InteractiveFiction.Business.Existence;

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

        public bool Is(string id)
        {
            return false;
        }

        public Location GetLocation()
        {
            return NullLocation.Instance;
        }

        public string GetFullDescription()
        {
            return "This doesn't exist!";
        }

        public string GetName()
        {
            return "This doesn't exist!";
        }

        public void SetLocation(Location location)
        {
        }

        public void SetUniverse(IUniverse universe)
        {
        }

        public Guid GetId()
        {
            return Guid.Empty;
        }
    }
}
