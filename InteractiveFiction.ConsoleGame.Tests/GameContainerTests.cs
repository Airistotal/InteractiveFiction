using InteractiveFiction.Business.Entity;
using InteractiveFiction.Business.Existence;
using InteractiveFiction.Business.Infrastructure;
using InteractiveFiction.Business.Infrastructure.MessageBus;
using InteractiveFiction.Business.Infrastructure.MessageBus.Message;
using InteractiveFiction.Business.Procedure;
using InteractiveFiction.Business.Procedure.Argument;
using InteractiveFiction.ConsoleGame.Sanitize.Commands;
using Moq;

namespace InteractiveFiction.ConsoleGame.Tests
{
    public class GameContainerTests
    {
        [Fact]
        public void When_GameCharacterSelected_CreateCharacter()
        {
            var entityBuilder = new Mock<IEntityBuilder>();
            var messageBus = new Mock<IMessageBus>();
            var sut = CreateGameContainer(messageBus: messageBus, entityBuilder: entityBuilder);

            sut.HandleCharacterInfoSelected(new CharacterInfoSelected() { Name = "John" });

            messageBus.Verify(_ => _.Register<GameArchetypeSelected>(It.IsAny<Action<IMessage>>()), Times.Once);
            entityBuilder.Verify(
                _ => _.FromLines(It.Is<IEnumerable<string>>(
                    _ => _.Contains("Name:John") && _.Contains("Type:CHARACTER") && _.Contains("DefaultCapabilities:Look"))),
                Times.Once);
        }

        [Fact]
        public void When_GameArchetypeSelected_BuildsNewUniverse()
        {
            var universeBuilder = new Mock<IUniverseBuilder>();
            var messageBus = new Mock<IMessageBus>();
            var sut = CreateGameContainer(universeBuilder: universeBuilder, messageBus: messageBus);

            sut.HandleGameArchetypeSelected(new GameArchetypeSelected(new GameArchetype() { Name = "Arbora" }));

            messageBus.Verify(_ => _.Register<GameArchetypeSelected>(It.IsAny<Action<IMessage>>()), Times.Once);
            universeBuilder.Verify(_ => _.Create("Arbora"), Times.Once);
        }

        [Fact]
        public void When_CharacterAndGameIsReady_SendsMoveToGameMessage()
        {
            var universe = new Mock<IUniverse>();
            var messageBus = new Mock<IMessageBus>();
            var sut = CreateGameContainer(messageBus: messageBus, universe: universe);

            sut.HandleCharacterInfoSelected(new CharacterInfoSelected() { Name = "John" });
            sut.HandleGameArchetypeSelected(new GameArchetypeSelected(new GameArchetype() { Name = "Arbora" }));

            universe.Verify(_ => _.Spawn(It.IsAny<IEntity>()), Times.Once);
            messageBus.Verify(_ => _.Publish(It.IsAny<MoveToGameMessage>()), Times.Once);
        }

        [Fact]
        public void When_GameAndCharacterIsReady_SendsMoveToGameMessage()
        {
            var universe = new Mock<IUniverse>();
            var messageBus = new Mock<IMessageBus>();
            var sut = CreateGameContainer(messageBus: messageBus, universe: universe);

            sut.HandleGameArchetypeSelected(new GameArchetypeSelected(new GameArchetype() { Name = "Arbora" }));
            sut.HandleCharacterInfoSelected(new CharacterInfoSelected() { Name = "John" });

            universe.Verify(_ => _.Spawn(It.IsAny<IEntity>()), Times.Once);
            messageBus.Verify(_ => _.Publish(It.IsAny<MoveToGameMessage>()), Times.Once);
        }

        [Fact]
        public void When_GetScreen_FromCharacterEvents_ClearsEvents()
        {
            var evt = "evt";
            var character = new Mock<IEntity>();
            character.Setup(_ => _.GetNewEvents()).Returns(new List<string>() { evt });
            var sut = CreateGameContainer(character: character, withGameAndCharacter: true);

            var screen = sut.GetScreen();

            character.Verify(_ => _.GetNewEvents(), Times.Once);
            character.Verify(_ => _.ArchiveEvents(), Times.Once);
            Assert.Contains(evt, screen);
        }

        [Fact]
        public void When_Perfom_ParsesInputForCharacter()
        {
            var character = new Mock<IEntity>();
            var procedureCommandParser = new Mock<IProcedureCommandParser>();
            var sut = CreateGameContainer(procedureCommandParser: procedureCommandParser,
                character: character, withGameAndCharacter: true);

            sut.Perform("");

            character.Verify(_ => _.Perform(It.IsAny<ProcedureType>(), It.IsAny<List<IProcedureArg>>()), Times.Once);
        }

        [Fact]
        public void When_Tick_TicksUniverse()
        {
            var universe = new Mock<IUniverse>();
            var sut = CreateGameContainer(universe: universe, withGameAndCharacter: true);

            sut.Tick();

            universe.Verify(_ => _.Tick(), Times.Once);
        }

        private static GameContainer CreateGameContainer(
            Mock<IMessageBus>? messageBus = null,
            Mock<IUniverseBuilder>? universeBuilder = null,
            Mock<IUniverse>? universe = null,
            Mock<IEntityBuilder>? entityBuilder = null,
            Mock<IEntity>? character = null,
            Mock<IProcedureCommandParser>? procedureCommandParser = null,
            bool withGameAndCharacter = false)
        {
            messageBus ??= new Mock<IMessageBus>();
            universe ??= new Mock<IUniverse>();
            universeBuilder ??= new Mock<IUniverseBuilder>();
            universeBuilder.Setup(_ => _.Create(It.IsAny<string>())).Returns(universe.Object);

            character ??= new Mock<IEntity>();
            entityBuilder ??= new Mock<IEntityBuilder>();
            entityBuilder.Setup(_ => _.FromLines(It.IsAny<IEnumerable<string>>())).Returns(entityBuilder.Object);
            entityBuilder.Setup(_ => _.Build()).Returns(character.Object);

            procedureCommandParser ??= new Mock<IProcedureCommandParser>();
            procedureCommandParser.Setup(_ => _.Parse(It.IsAny<string>())).Returns(
                new ProcedureCommand()
                {
                    ProcedureType = ProcedureType.NULL
                });

            var gameContainer = new GameContainer(
                messageBus.Object, universeBuilder.Object, entityBuilder.Object, procedureCommandParser.Object);

            if (withGameAndCharacter)
            {
                gameContainer.HandleGameArchetypeSelected(new GameArchetypeSelected(new GameArchetype() { Name = "Arbora" }));
                gameContainer.HandleCharacterInfoSelected(new CharacterInfoSelected() { Name = "John" });
            }

            return gameContainer;
        }
    }
}
