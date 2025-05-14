using InteractiveFiction.Business.Entity;
using InteractiveFiction.Business.Entity.Locations;

namespace InteractiveFiction.WorldBuilder.Business.World
{
    public class World
    {
        public string Name { get; }

        public IList<Location> Locations { get; }

        public IList<IEntity> Entities { get; }

        public World(string name)
        {
            Name = name;
            Locations = new List<Location>();
            Entities = new List<IEntity>();
        }
    }
}
