using InteractiveFiction.Business.Entity;
using InteractiveFiction.Business.Existence;
using InteractiveFiction.Business.Infrastructure;
using InteractiveFiction.Business.Infrastructure.MessageBus;
using InteractiveFiction.Business.Infrastructure.MessageBus.Message;
using InteractiveFiction.Business.Procedure;
using InteractiveFiction.ConsoleGame.Menu;
using Moq;

namespace InteractiveFiction.ConsoleGame.Tests
{
    public class ConsoleGameRunnerTests
    {
        [Fact]
        public void When_CreateGame_StartsMenu()
        {
            var menu = new Mock<IGameMenu>();
            var sut = CreateConsoleGameRunner(menu);

            menu.Verify(_ => _.Perform("boot"), Times.Once);
        }

        [Fact]
        public void When_InMenu_InteractsWithMenu()
        {
            var gameContainer = new Mock<IGameContainer>();
            var menu = new Mock<IGameMenu>();
            var sut = CreateConsoleGameRunner(menu, gameContainer: gameContainer);

            sut.Perform("1");
            sut.GetScreen();
            sut.Tick();

            menu.Verify(_ => _.Perform("1"), Times.Once);
            menu.Verify(_ => _.GetScreen(), Times.Once);
            gameContainer.Verify(_ => _.Tick(), Times.Never);
        }

        [Fact]
        public void When_MoveToGame_GameContainerNotReady_ThrowsException()
        {
            var gameContainer = new Mock<IGameContainer>();
            var sut = CreateConsoleGameRunner(gameContainer: gameContainer);
            gameContainer.Setup(_ => _.IsReady()).Returns(false);

            Assert.Throws<CantStartGameException>(() => sut.HandleMoveToGame(new MoveToGameMessage()));
        }

        [Fact]
        public void When_InGame_InteractsWithGameContainer()
        {
            var gameContainer = new Mock<IGameContainer>();
            var messageBus = new Mock<IMessageBus>();
            var sut = CreateConsoleGameRunner(messageBus: messageBus, gameContainer: gameContainer);
            sut.HandleMoveToGame(new MoveToGameMessage());

            sut.Perform("m w");
            sut.GetScreen();
            sut.Tick();

            messageBus.Verify(_ => _.Register<MoveToGameMessage>(It.IsAny<Action<IMessage>>()), Times.Once);
            gameContainer.Verify(_ => _.Perform("m w"), Times.Once);
            gameContainer.Verify(_ => _.GetScreen(), Times.Once);
            gameContainer.Verify(_ => _.Tick(), Times.Once);
        }

        private static ConsoleGameRunner CreateConsoleGameRunner(
            Mock<IGameMenu>? menu = null,
            Mock<IMessageBus>? messageBus = null,
            Mock<IGameContainer>? gameContainer = null)
        {
            menu ??= new Mock<IGameMenu>();
            gameContainer ??= new Mock<IGameContainer>();
            gameContainer.Setup(_ => _.IsReady()).Returns(true);
            messageBus ??= new Mock<IMessageBus>();

            return new ConsoleGameRunner(messageBus.Object, menu.Object, gameContainer.Object);
        }
    }
}
