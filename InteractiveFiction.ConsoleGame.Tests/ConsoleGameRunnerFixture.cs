using InteractiveFiction.Business.Infrastructure.Game;
using InteractiveFiction.Business.Infrastructure.MessageBus;
using InteractiveFiction.Business.Infrastructure.MessageBus.Message;
using InteractiveFiction.Business.Procedure;
using InteractiveFiction.ConsoleGame.Menu;
using InteractiveFiction.ConsoleGame.Sanitize.Commands;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace InteractiveFiction.ConsoleGame.Tests
{
    internal class ConsoleGameRunnerFixture
    {
        private readonly Mock<IGameMenu> menu = new();
        private readonly Mock<IMessageBus> messageBus = new();
        private readonly Mock<IGameContainer> gameContainer = new();
        private readonly Mock<IProcedureCommandParser> commandParser = new();

        private ConsoleGameRunner? sut;

        private ConsoleGameRunnerFixture() { }

        public static ConsoleGameRunnerFixture GetFixture() { return new ConsoleGameRunnerFixture(); }

        public ConsoleGameRunnerFixture GameContainerReady(bool ready)
        {
            gameContainer.Setup(_ => _.IsReady()).Returns(ready);

            return this;
        }

        [MemberNotNull(nameof(sut))]
        public ConsoleGameRunnerFixture Init()
        {
            sut = new ConsoleGameRunner(messageBus.Object, menu.Object, gameContainer.Object, commandParser.Object);

            return this;
        }

        public ConsoleGameRunnerFixture MoveToGame()
        {
            if (sut == null)
            {
                Init();
            }

            sut.HandleMoveToGame(new MoveToGameMessage());

            return this;
        }

        public ConsoleGameRunnerFixture Interact()
        {
            if (sut == null)
            {
                Init();
            }

            sut.Perform("1");
            sut.GetScreen();
            sut.Tick();

            return this;
        }

        public void AssertPerformed(string command)
        {
            menu.Verify(_ => _.Perform(command), Times.Once);
        }

        public void AssertInteractedWithMenu()
        {
            menu.Verify(_ => _.Perform("1"), Times.Once);
            menu.Verify(_ => _.GetScreen(), Times.Once);
            gameContainer.Verify(_ => _.Tick(), Times.Never);
        }

        public void AssertInteractedWithGame()
        {
            menu.Verify(_ => _.Perform("1"), Times.Never);
            menu.Verify(_ => _.GetScreen(), Times.Never);
            gameContainer.Verify(_ => _.Perform(It.IsAny<ProcedureCommand>()), Times.Once);
            gameContainer.Verify(_ => _.GetScreen(), Times.Once);
            gameContainer.Verify(_ => _.Tick(), Times.Once);
        }

        public void AssertMoveToGameThrows<T>() where T : Exception
        {
            Init();

            Assert.Throws<T>(() => sut.HandleMoveToGame(new MoveToGameMessage()));
        }
    }
}
