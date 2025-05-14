using InteractiveFiction.ConsoleGame.Menu;
using InteractiveFiction.ConsoleGame.Menu.State;
using InteractiveFiction.ConsoleGame.Sanitize.Commands;
using Moq;

namespace InteractiveFiction.ConsoleGame.Tests.Menu
{
    public class GameMenuTests
    {
        [Fact]
        public void When_StartMenu_CreatesMainMenuState()
        {
            var mockMenuStateFactory = new Mock<IMenuStateFactory>();
            var gameMenu = CreateGameMenu(mockMenuStateFactory: mockMenuStateFactory);

            gameMenu.Perform("boot");

            mockMenuStateFactory.Verify(_ => _.GetInstance(MenuStateType.MainMenu), Times.Once);
        }

        [Fact]
        public void When_MenuStarted_PassesParsedCommandToMenuState()
        {
            var mockMenuState = new Mock<IMenuState>();
            var mockCommandParser = new Mock<ICommandParser>();
            var gameMenu = CreateGameMenu(mockMenuState, mockCommandParser: mockCommandParser);

            gameMenu.Perform("boot");
            gameMenu.Perform("1");

            mockCommandParser.Verify(_ => _.Parse("boot"), Times.Once);
            mockCommandParser.Verify(_ => _.Parse("1"), Times.Once);
            mockMenuState.Verify(_ => _.Transition(Command.ACTION1, "1"), Times.Once);
        }

        [Fact]
        public void When_GetScreen_GetScreenFromMenuState()
        {
            var mockMenuState = new Mock<IMenuState>();
            var gameMenu = CreateGameMenu(mockMenuState);

            gameMenu.Perform("boot");
            gameMenu.GetScreen();

            mockMenuState.Verify(_ => _.GetScreen(), Times.Once);
        }

        private static GameMenu CreateGameMenu(
            Mock<IMenuState>? mockMenuState = null, 
            Mock<IMenuStateFactory>? mockMenuStateFactory = null,
            Mock<ICommandParser>? mockCommandParser = null)
        {
            if (mockMenuState == null)
            {
                mockMenuState = new Mock<IMenuState>();
            }

            if (mockMenuStateFactory == null)
            {
                mockMenuStateFactory = new Mock<IMenuStateFactory>();
            }
            mockMenuStateFactory.Setup(_ => _.GetInstance(It.IsAny<MenuStateType>())).Returns(mockMenuState.Object);

            if (mockCommandParser == null)
            {
                mockCommandParser = new Mock<ICommandParser>();
            }
            mockCommandParser.Setup(_ => _.Parse("boot")).Returns(Command.BOOT);
            mockCommandParser.Setup(_ => _.Parse("1")).Returns(Command.ACTION1);

            return new GameMenu(mockMenuStateFactory.Object, mockCommandParser.Object);
        }
    }
}