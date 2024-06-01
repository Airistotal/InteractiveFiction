using InteractiveFiction.Business.Entity;
using InteractiveFiction.Business.Existence;
using InteractiveFiction.Business.Infrastructure;
using InteractiveFiction.Business.Infrastructure.MessageBus;
using InteractiveFiction.Business.Infrastructure.MessageBus.Message;
using InteractiveFiction.Business.Procedure;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace InteractiveFiction.ConsoleGame.Tests
{
    internal class GameContainerFixture
    {
        private readonly Mock<IEntityBuilder> entityBuilder = new();
        private readonly Mock<IMessageBus> messageBus = new();
        private readonly Mock<IUniverseBuilder> universeBuilder = new();
        private readonly Mock<IUniverse> universe = new();
        private readonly Mock<IAgent> character = new();

        private GameContainer sut;

        private readonly string evt = "evt";
        private string screen; 

        private GameContainerFixture() {
            CreateGameContainer();
        }

        public static GameContainerFixture GetFixture()
        {
            return new GameContainerFixture();
        }

        public GameContainerFixture WithCharacterEvent()
        {
            character.Setup(_ => _.GetNewEvents()).Returns(new List<string>() { evt });

            return this;
        }

        public GameContainerFixture HandleCharacterInfoSelected()
        {
            sut.HandleCharacterInfoSelected(new CharacterInfoSelected() { Name = "John" });

            return this;
        }

        public GameContainerFixture HandleGameArchetypeSelected()
        {
            sut.HandleGameArchetypeSelected(new GameArchetypeSelected(new GameArchetype() { Name = "Arbora" }));

            return this;
        }

        public GameContainerFixture ReadyCharacterAndGame()
        {
            HandleCharacterInfoSelected();
            HandleGameArchetypeSelected();

            return this;
        }

        public GameContainerFixture GetScreen()
        {
            screen = sut.GetScreen();

            return this;
        }

        public GameContainerFixture Perform()
        {
            sut.Perform(new ProcedureCommand());

            return this;
        }

        public GameContainerFixture Tick()
        {
            sut.Tick();

            return this;
        }

        public void AssertCharacterCreated()
        {
            messageBus.Verify(_ => _.Register<GameArchetypeSelected>(It.IsAny<Action<IMessage>>()), Times.Once);
            entityBuilder.Verify(_ => _.Character("John"), Times.Once);
        }

        public void AssertGameArchetypeSelected()
        {
            messageBus.Verify(_ => _.Register<GameArchetypeSelected>(It.IsAny<Action<IMessage>>()), Times.Once);
            universeBuilder.Verify(_ => _.Create("Arbora"), Times.Once);
        }

        public void AssertMovedToGame()
        {
            universe.Verify(_ => _.Spawn(It.IsAny<IAgent>()), Times.Once);
            messageBus.Verify(_ => _.Publish(It.IsAny<MoveToGameMessage>()), Times.Once);
        }

        public void AssertScreenHasCharacterEvent()
        {
            character.Verify(_ => _.GetNewEvents(), Times.Once);
            character.Verify(_ => _.ArchiveEvents(), Times.Once);
            Assert.Contains(evt, screen);
        }

        public void AssertInputParsedForCharacter()
        {
            character.Verify(_ => _.Perform(It.IsAny<ProcedureType>(), It.IsAny<List<IProcedureArg>>()), Times.Once);
        }

        public void AssertUniverseTicked()
        {
            universe.Verify(_ => _.Tick(), Times.Once);
        }

        [MemberNotNull(nameof(sut))]
        private void CreateGameContainer()
        {
            universeBuilder.Setup(_ => _.Create(It.IsAny<string>())).Returns(universe.Object);

            character.As<IAgent>();
            entityBuilder.Setup(_ => _.From(It.IsAny<string>())).Returns(entityBuilder.Object);
            entityBuilder.Setup(_ => _.Character(It.IsAny<string>())).Returns(entityBuilder.Object);
            entityBuilder.Setup(_ => _.Build()).Returns(character.As<IEntity>().Object);

            sut = new GameContainer(
                messageBus.Object, universeBuilder.Object, entityBuilder.Object);
        }
    }
}
