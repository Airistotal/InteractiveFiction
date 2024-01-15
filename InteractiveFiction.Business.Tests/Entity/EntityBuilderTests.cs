using InteractiveFiction.Business.Entity;
using InteractiveFiction.Business.Entity.AnimateEntities;
using InteractiveFiction.Business.Entity.Locations;
using InteractiveFiction.Business.Existence;
using InteractiveFiction.Business.Goal;
using InteractiveFiction.Business.Procedure;
using InteractiveFiction.Business.Tests.Utils;
using Moq;
using System.IO.Abstractions;

namespace InteractiveFiction.Business.Tests.Entity
{
    public class EntityBuilderTests
    {
        [Fact]
        public void When_BuildCharacter_HasAllAttributes()
        {
            var filePath = "path";
            var fileSystem = GetFileSystemMock(filePath,
                "{" +
                "   \"Type\":\"CHARACTER\"," +
                "   \"Name\":\"King Leon\"," +
                "   \"Description\":\"The King of the Misty Castle\"," +
                "   \"Birthdate\":\"Unknown\"," +
                "   \"Parents\":[]," +
                "   \"Children\":[]," +
                "   \"Health\":2400," +
                "   \"Strength\":1," +
                "   \"Speed\":2," +
                "   \"Dexterity\":3," +
                "   \"Endurance\":4," +
                "   \"Restraint\":5," +
                "   \"Discretion\":6," +
                "   \"Courage\":7," +
                "   \"Fairness\":8," +
                "   \"Compassion\":9," +
                "   \"Hope\":10," +
                "   \"Groundedness\":11," +
                "}");
            var entityBuilder = GetEntityBuilder(fileSystem);

            var entity = (Character) entityBuilder.From(filePath).Build();

            Assert.Equal("King Leon", entity.Name);
            Assert.Equal("The King of the Misty Castle", entity.Description);
            Assert.Equal("Unknown", entity.Birthdate);
            Assert.Empty(entity.Parents);
            Assert.Empty(entity.Children);
            Assert.Equal(2400, entity.Health);
            Assert.Equal(1, entity.Strength);
            Assert.Equal(2, entity.Speed);
            Assert.Equal(3, entity.Dexterity);
            Assert.Equal(4, entity.Endurance);
            Assert.Equal(5, entity.Restraint);
            Assert.Equal(6, entity.Discretion);
            Assert.Equal(7, entity.Courage);
            Assert.Equal(8, entity.Fairness);
            Assert.Equal(9, entity.Compassion);
            Assert.Equal(10, entity.Hope);
            Assert.Equal(11, entity.Groundedness);
        }

        [Fact]
        public void When_BuildEntity_CreatesDefaultCapabilities()
        {
            var filePath = "path";
            var fileSystem = GetFileSystemMock(filePath,
                "{" +
                "   \"Type\":\"CHARACTER\"," +
                "   \"Name\":\"Name\"," +
                "   \"Health\":\"123\"," +
                $"  \"DefaultCapabilities\":\"{ProcedureType.Look},{ProcedureType.Move},{ProcedureType.Attack}\"," +
                "}");
            var procedureBuilder = DefaultMocks.GetProcedureBuilderMock();
            var entityBuilder = GetEntityBuilder(fileSystem, procedureBuilder);
            var entity = (Character)entityBuilder.From(filePath).Build();
            entity.Universe = new Mock<IUniverse>().Object;

            entity.Perform(ProcedureType.Look, new List<IProcedureArg>());

            procedureBuilder.Verify(_ => _.Type(It.Is<ProcedureType>(_ => _ == ProcedureType.Look)), Times.Once);
            procedureBuilder.Verify(_ => _.Type(It.Is<ProcedureType>(_ => _ == ProcedureType.Move)), Times.Once);
            procedureBuilder.Verify(_ => _.Type(It.Is<ProcedureType>(_ => _ == ProcedureType.Attack)), Times.Once);
        }

        [Fact]
        public void When_BuildLocation_HasAllAttributes()
        {
            var filePath = "path";
            var fileSystem = GetFileSystemMock(filePath,
                "{" +
                "   \"Type\":\"LOCATION\"," +
                "   \"Title\":\"Misty Castle\"," +
                "   \"Description\":\"This is the Misty Castle.The centre of the Kingdom and the seat of power.\"," +
                "   \"LocationType\":\"Building\"," +
                "   \"Entities\":\"KingLeon\"" +
                "}");
            var entityBuilder = GetEntityBuilder(fileSystem);

            var entity = (Location)entityBuilder.From(filePath).Build();

            Assert.Equal("Misty Castle", entity.Title);
            Assert.Equal("This is the Misty Castle.The centre of the Kingdom and the seat of power.", entity.Description);
            Assert.Equal(LocationType.Building, entity.Type);
            Assert.Equal(new List<string> { "KingLeon" }, entity.EntityNames);
        }

        [Fact]
        public void When_BuildCharacterPrototype_BuildsFromPrototypeFile()
        {
            var filePath = "res/prototype/character.json";
            var fileSystem = GetFileSystemMock(filePath,
                "{" +
                "   \"Type\":\"CHARACTER\"," +
                "   \"Name\":\"Name\"," +
                "   \"Health\":\"123\"," +
                $"  \"DefaultCapabilities\":\"{ProcedureType.Look},{ProcedureType.Move},{ProcedureType.Attack}\"," +
                "}");
            var entityBuilder = GetEntityBuilder(fileSystem);

            var entity = (Character)entityBuilder.Character("name").Build();

            Assert.Equal("name", entity.GetName());
            fileSystem.Verify(_ => _.File.ReadAllText(filePath), Times.Once);
        }

        private EntityBuilder GetEntityBuilder(Mock<IFileSystem> fileSystem, Mock<IProcedureBuilder>? procedureBuilder = null)
        {


            return new EntityBuilder(
                DefaultMocks.GetTrackerFactoryMock().Object,
                procedureBuilder?.Object ?? DefaultMocks.GetProcedureBuilderMock().Object, 
                DefaultMocks.GetTextDecorator().Object, 
                fileSystem.Object);
        }

        private Mock<IFileSystem> GetFileSystemMock(string path, string fileContent)
        {
            var file = new Mock<IFile>();
            file.Setup(_ => _.ReadAllText(path)).Returns(fileContent);
            var fileSystem = new Mock<IFileSystem>();
            fileSystem.Setup(_ => _.File).Returns(file.Object);

            return fileSystem;
        }
    }
}
