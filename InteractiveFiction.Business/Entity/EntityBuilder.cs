using InteractiveFiction.Business.Entity.AnimateEntities;
using InteractiveFiction.Business.Entity.Locations;
using InteractiveFiction.Business.Existence;
using InteractiveFiction.Business.Infrastructure;
using InteractiveFiction.Business.Procedure;
using Newtonsoft.Json.Linq;
using System.Diagnostics.CodeAnalysis;
using System.IO.Abstractions;

namespace InteractiveFiction.Business.Entity
{
    public class EntityBuilder : IEntityBuilder
    {
        private JObject? loadedFile;
        private string? nameOverride;
        private EntityType type;
        private readonly IProcedureBuilder procedureBuilder;
        private readonly ITextDecorator textDecorator;
        private readonly IFileSystem fileSystem;

        public EntityBuilder(
            IProcedureBuilder procedureBuilder, 
            ITextDecorator textDecorator,
            IFileSystem fileSystem)
        {
            this.procedureBuilder = procedureBuilder;
            this.textDecorator = textDecorator;
            this.fileSystem = fileSystem;
        }

        public IEntityBuilder From(string path)
        {
            loadedFile = JObject.Parse(fileSystem.File.ReadAllText(path));
            type = (EntityType)Enum.Parse(typeof(EntityType), loadedFile.SelectToken("Type").Value<string>());

            return this;
        }

        public IEntityBuilder Character(string? name)
        {
            nameOverride = name;
            From(@"res/prototype/character.json");

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
            CheckLoadedFile();

            var character = new Character(procedureBuilder)
            {
                Name = nameOverride ?? loadedFile.SelectToken("Name").Value<string>(),
                Description = loadedFile.SelectToken("Description")?.Value<string>() ?? "",
                Birthdate = loadedFile.SelectToken("Birthdate")?.Value<string>() ?? "",
                Health =  loadedFile.SelectToken("Health").Value<int>(),
                Strength = loadedFile.SelectToken("Strength")?.Value<int>() ?? 1,
                Speed = loadedFile.SelectToken("Speed")?.Value<int>() ?? 1,
                Dexterity = loadedFile.SelectToken("Dexterity")?.Value<int>() ?? 1,
                Endurance = loadedFile.SelectToken("Endurance")?.Value<int>() ?? 1,
                Restraint = loadedFile.SelectToken("Restraint")?.Value<int>() ?? 1,
                Discretion = loadedFile.SelectToken("Discretion")?.Value<int>() ?? 1,
                Courage = loadedFile.SelectToken("Courage")?.Value<int>() ?? 1,
                Fairness = loadedFile.SelectToken("Fairness")?.Value<int>() ?? 1,
                Compassion = loadedFile.SelectToken("Compassion")?.Value<int>() ?? 1,
                Hope = loadedFile.SelectToken("Hope")?.Value<int>() ?? 1,
                Groundedness = loadedFile.SelectToken("Groundedness")?.Value<int>() ?? 1,
            };

            AddCapabilities(character, loadedFile.SelectToken("DefaultCapabilities")?.Value<string>() ?? "");

            return character;
        }

        private static void AddCapabilities(Character character, string value)
        {
            var procedureTypes = value.Split(",").Where(_ => !string.IsNullOrWhiteSpace(_));

            foreach (var procedureName in procedureTypes)
            {
                var procedureType = Enum.Parse<ProcedureType>(procedureName);
                character.AddCapability(procedureType);
            }
        }

        private Location MakeLocation()
        {
            CheckLoadedFile();

            return new Location(textDecorator)
            {
                Title = loadedFile.SelectToken("Title").Value<string>(),
                Description = loadedFile.SelectToken("Description").Value<string>(),
                Type = (LocationType)Enum.Parse(typeof(LocationType), loadedFile.SelectToken("LocationType").Value<string>()),
                EntityNames = loadedFile.SelectToken("Entities").Value<string>().Split(',').ToList(),
            };
        }

        [MemberNotNull(nameof(loadedFile))]
        private void CheckLoadedFile()
        {
            if (loadedFile == null)
            {
                throw new Exception("Unable to build entity without a file");
            }
        }
    }
}
