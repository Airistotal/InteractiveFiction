using InteractiveFiction.Business.Existence;
using InteractiveFiction.Business.Infrastructure;

namespace InteractiveFiction.Business.Entity.Locations
{
    public class Location : IEntity
    {
        private readonly ITextDecorator textDecorator;

        public IUniverse? Universe;

        public Guid Id { get; } = Guid.NewGuid();
        public List<IEntity> Children { get; } = new List<IEntity>();
        public Dictionary<Direction, Location> Paths { get; } = new();

        public LocationType Type { get; set; }
        public string? Description { get; set; }
        public string? Title { get; set; }
        public List<string> EntityNames { get; set; } = new();

        public Location(ITextDecorator textDecorator)
        {
            this.textDecorator = textDecorator;
        }

        public void AddPath(Direction direction, Location locationInDirection)
        {
            if (locationInDirection == null)
            {
                return;
            }

            Paths.Add(direction, locationInDirection);
        }

        public Location Go(Direction direction)
        {
            if (!Paths.ContainsKey(direction))
            {
                return NullLocation.Instance;
            }

            return Paths[direction];
        }

        public void DestroyPath(Direction direction)
        {
            Paths.Remove(direction);
        }

        public virtual string GetFullDescription()
        {
            return textDecorator.Underline(Title ?? "No Title") + Environment.NewLine +
                Description + Environment.NewLine + Environment.NewLine +
                GetChildList() +
                GetDirections();
        }

        private string GetChildList()
        {
            if (Children.Count <= 0)
            {
                return "";
            }

            var sentence = $"Here you can see ";
            var childNames = Children.Select(_ => _.GetName()).ToList();
            for (var i = 0; i < childNames.Count; i++)
            {
                if (i == 0)
                {
                    sentence += childNames[i];
                }
                else if (i < childNames.Count - 1)
                {
                    sentence += ", " + childNames[i];
                }
                else
                {
                    sentence += (i != 1 ? "," : "") + " and " + childNames[i]; 
                }
            }

            return sentence + $".{Environment.NewLine}{Environment.NewLine}";
        }

        public string GetDirections()
        {
            var directions = $"You can move:{Environment.NewLine}";

            foreach (var direction in Paths)
            {
                directions += $"  {direction.Key}: {direction.Value.Title}{Environment.NewLine}";
            }

            return directions;
        }

        public override int GetHashCode()
        {
            return Type.GetHashCode() +
                Title?.GetHashCode() ?? 0 +
                Description?.GetHashCode() ?? 0 +
                EntityNames.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            if (obj is Location loc)
            {
                return string.Equals(Title, loc.Title) &&
                    string.Equals(Description, loc.Description) &&
                    Paths.Count == Paths.Count &&
                    !Paths.Except(loc.Paths).Any() &&
                    EntityNames.Count == EntityNames.Count &&
                    !EntityNames.Except(loc.EntityNames).Any() &&
                    Type.Equals(loc.Type);
            }
            else
            {
                return false;
            }
        }

        public bool Is(string id)
        {
            return
                !string.IsNullOrWhiteSpace(Title) && Title.Equals(id) ||
                Guid.TryParse(id, out var guid) && Id.Equals(guid);
        }

        public IEntity GetTarget(string target)
        {
            foreach (var entity in Children)
            {
                if (entity.Is(target))
                {
                    return entity;
                }
            }

            return NullEntity.Instance;
        }

        public string GetName()
        {
            return Title ?? "";
        }

        public void SetLocation(Location location)
        {
            throw new NotImplementedException();
        }

        public void SetUniverse(IUniverse universe)
        {
            this.Universe = universe;
        }

        public Location GetLocation()
        {
            return this;
        }

        public Guid GetId()
        {
            return Id;
        }
    }
}
