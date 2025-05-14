using InteractiveFiction.Business.Existence;
using InteractiveFiction.Business.Infrastructure;
using InteractiveFiction.Business.Procedure;
using InteractiveFiction.Business.Procedure.Argument;

namespace InteractiveFiction.Business.Entity
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

        public Location(ITextDecorator textDecorator) {
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

        public string GetDirections()
        {
            var directions = $"You can move:{Environment.NewLine}";

            foreach (var direction in Paths)
            {
                directions += $"  {direction.Key}: {direction.Value.Title}{Environment.NewLine}";
            }

            return directions;
        }

        public virtual string GetFullDescription()
        {
            return textDecorator.Underline(Title ?? "No Title") + Environment.NewLine +
                Description + Environment.NewLine + Environment.NewLine +
                GetDirections();
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

        public void Perform(ProcedureType type, List<IProcedureArg> args)
        {
            throw new NotImplementedException();
        }

        public void ArchiveEvents()
        {
            throw new NotImplementedException();
        }

        public void AddEvent(string evt)
        {
            throw new NotImplementedException();
        }

        public void SetLocation(Location location)
        {
            throw new NotImplementedException();
        }

        public Location GetLocation()
        {
            return this;
        }

        public void SetUniverse(IUniverse universe)
        {
            Universe = universe;
        }

        public List<string> GetNewEvents()
        {
            throw new NotImplementedException();
        }

        public bool Is(string id)
        {
            return 
                (!string.IsNullOrWhiteSpace(Title) && Title.Equals(id)) ||
                (Guid.TryParse(id, out var guid) && Id.Equals(guid));
        }

        public IEntity GetTarget(string target)
        {
            foreach(var entity in Children)
            {
                if (entity.Is(target))
                {
                    return entity;
                }
            }

            return NullEntity.Instance;
        }
    }
}
