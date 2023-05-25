using InteractiveFiction.Business.Existence;
using InteractiveFiction.Business.Infrastructure;
using InteractiveFiction.Business.Procedure;

namespace InteractiveFiction.Business.Entity
{
    public class EntityBuilder : IEntityBuilder
    {
        private IEnumerable<string> lines = new List<string>();
        private EntityType type;
        private readonly IProcedureBuilder procedureBuilder;
        private readonly ITextDecorator textDecorator;

        public EntityBuilder(IProcedureBuilder procedureBuilder, ITextDecorator textDecorator)
        {
            this.procedureBuilder = procedureBuilder;
            this.textDecorator = textDecorator;
        }

        public IEntityBuilder FromLines(IEnumerable<string> lines)
        {
            foreach (var line in lines)
            {
                if (line.StartsWith("Type"))
                {
                    type = (EntityType)Enum.Parse(typeof(EntityType), line.Split(":")[1]);
                }
                else
                {
                    this.lines = this.lines.Append(line);
                }
            }

            return this;
        }

        public IEntity Build()
        {
            return type switch
            {
                EntityType.CHARACTER => MakeCharacter(),
                EntityType.LOCATION => MakeLocation(),
                _ => throw new Exception("Unable to make " + type),
            };
        }

        private Character MakeCharacter()
        {
            var character = new Character(procedureBuilder, textDecorator);

            foreach (var line in lines)
            {
                var split = line.Split(':');
                if (split.Length != 2) { continue; }

                var property = split[0].Trim();
                var value = split[1].Trim();

                switch (property)
                {
                    case "Name":
                        character.Name = value;
                        break;
                    case "Description":
                        character.Description = value;
                        break;
                    case "Birthdate":
                        character.Birthdate = value;
                        break;
                    case "Parents":
                    case "Children":
                        break;
                    case "Health":
                        character.Health = int.Parse(value);
                        break;
                    case "Strength":
                        character.Strength = int.Parse(value);
                        break;
                    case "Speed":
                        character.Speed = int.Parse(value);
                        break;
                    case "Dexterity":
                        character.Dexterity = int.Parse(value);
                        break;
                    case "Endurance":
                        character.Endurance = int.Parse(value);
                        break;
                    case "Restraint":
                        character.Restraint = int.Parse(value);
                        break;
                    case "Discretion":
                        character.Discretion = int.Parse(value);
                        break;
                    case "Courage":
                        character.Courage = int.Parse(value);
                        break;
                    case "Fairness":
                        character.Fairness = int.Parse(value);
                        break;
                    case "Compassion":
                        character.Compassion = int.Parse(value);
                        break;
                    case "Hope":
                        character.Hope = int.Parse(value);
                        break;
                    case "Groundedness":
                        character.Groundedness = int.Parse(value);
                        break;
                    case "DefaultCapabilities":
                        AddCapabilities(character, value);
                        break;
                }
            }

            return character;
        }

        private static void AddCapabilities(Character character, string value)
        {
            var procedureTypes = value.Split(",");

            foreach (var procedureName in procedureTypes)
            {
                var procedureType = Enum.Parse<ProcedureType>(procedureName);
                character.AddCapability(procedureType);
            }
        }

        private Location MakeLocation()
        {
            var location = new Location(textDecorator);

            foreach (var line in lines)
            {
                var split = line.Split(':');
                if (split.Length != 2) { continue; }

                var property = split[0].Trim();
                var value = split[1].Trim();

                switch(property)
                {
                    case "Title":
                        location.Title = value;
                        break;
                    case "Description":
                        location.Description = value;
                        break;
                    case "LocationType":
                        location.Type = (LocationType)Enum.Parse(typeof(LocationType), value);
                        break;
                    case "Entities":
                        location.EntityNames = value.Split(',').ToList();
                        break;
                }
            }

            return location;
        }
    }
}
