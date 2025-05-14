using InteractiveFiction.Business.Existence;
using InteractiveFiction.Business.Infrastructure;
using InteractiveFiction.Business.Infrastructure.MessageBus;
using InteractiveFiction.Business.Infrastructure.MessageBus.Message;
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

            menu.Verify(_ => _.Perform(Command.BOOT), Times.Once);
        }

        [Fact]
        public void When_BeforeGameAndCharacterLoaded_UsesMenuScreens()
        {
            var menu = new Mock<IGameMenu>();
            var sut = CreateConsoleGameRunner(menu);

            sut.GetScreen();

            menu.Verify(_ => _.GetScreen(), Times.Once);
        }

        [Fact]
        public void When_GameArchetypeSelected_BuildsNewUniverse()
        {
            var universeLoader = new Mock<IUniverseLoader>();
            var messageBus = new Mock<IMessageBus>();
            var sut = CreateConsoleGameRunner(universeLoader: universeLoader, messageBus: messageBus);

            messageBus.Object.Publish(new GameArchetypeSelected(new GameArchetype() { Name = "Arbora" }));
        }

        private ConsoleGameRunner CreateConsoleGameRunner(
            Mock<IGameMenu>? menu = null, 
            Mock<IUniverseLoader>? universeLoader = null,
            Mock<IMessageBus>? messageBus = null)
        {
            if (menu == null)
            {
                menu = new Mock<IGameMenu>();
            }

            if (universeLoader == null)
            {
                universeLoader = new Mock<IUniverseLoader>();
            }

            if (messageBus == null)
            {
                messageBus = new Mock<IMessageBus>();
            }

            return new ConsoleGameRunner(menu.Object, messageBus.Object, universeLoader.Object);
        }
    }
}
