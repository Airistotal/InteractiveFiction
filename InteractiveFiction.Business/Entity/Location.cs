using InteractiveFiction.Business.Existence;
using InteractiveFiction.Business.Procedure;

namespace InteractiveFiction.Business.Entity
{
    public class Location : InanimateEntity
    {
        public LocationType Type { get; set; }
        public string? Description { get; set; }
        public string? Title { get; set; }
        public List<string> EntityNames { get; set; } = new();

        private readonly Dictionary<Direction, Location> siblings = new();

        public Location(IProcedureBuilder? procedureBuilder) : base(procedureBuilder) { }

        public void AddPath(Direction direction, Location locationInDirection)
        {
            if (locationInDirection == null)
            {
                return;
            }

            siblings.Add(direction, locationInDirection);
        }

        public Location Go(Direction direction)
        {
            if (!siblings.ContainsKey(direction))
            {
                return Empty();
            }

            return siblings[direction];
        }

        public static Location Empty()
        {
            return new Location(null) {
                Type = LocationType.NULL,
                Title = "",
                Description = "This place doesn't exist.",
            };
        }

        public void DestroyPath(Direction direction)
        {
            siblings.Remove(direction);
        }
    }
}
