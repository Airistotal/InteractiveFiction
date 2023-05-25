using InteractiveFiction.Business.Entity;

namespace InteractiveFiction.Business.Existence
{
    public class Instant
    {
        public Location Spawn { get; }

        public Instant(Location spawn)
        {
            Spawn = spawn;
        }
    }
}
