using InteractiveFiction.Business.Entity.Locations;
using InteractiveFiction.Business.Existence;

namespace InteractiveFiction.Business.Entity
{
    public interface IEntity
    {
        bool Is(string id);
        Guid GetId();
        string GetName();
        string GetFullDescription();
        void SetLocation(Location location);
        void SetUniverse(IUniverse universe);
        Location GetLocation();
    }
}
