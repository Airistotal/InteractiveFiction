using InteractiveFiction.ConsoleGame.Menu;
using InteractiveFiction.ConsoleGame.Menu.State;
using Moq;

namespace InteractiveFiction.ConsoleGame.Tests.Menu
{
    public class GameMenuTests
    {
        [Fact]
        public void When_StartMenu_CreatesMainMenuState()
        {
            var mockMenuStateFactory = new Mock<IMenuStateFactory>();
            var gameMenu = new GameMenu(mockMenuStateFactory.Object);

            gameMenu.Perform(Command.BOOT);

            mockMenuStateFactory.Verify(_ => _.GetInstance(MenuStateType.MainMenu), Times.Once);
        }

        [Fact]
        public void When_MenuStarted_PassesCommandToMenuState()
        {
            var mockMenuState = new Mock<IMenuState>();
            var mockMenuStateFactory = new Mock<IMenuStateFactory>();
            mockMenuStateFactory.Setup(_ => _.GetInstance(It.IsAny<MenuStateType>())).Returns(mockMenuState.Object);
            var gameMenu = new GameMenu(mockMenuStateFactory.Object);

            gameMenu.Perform(Command.BOOT);
            gameMenu.Perform(Command.ACTION1);

            mockMenuState.Verify(_ => _.Transition(Command.ACTION1), Times.Once);
        }

        [Fact]
        public void When_GetScreen_GetScreenFromMenuState()
        {
            var mockMenuState = new Mock<IMenuState>();
            var mockMenuStateFactory = new Mock<IMenuStateFactory>();
            mockMenuStateFactory.Setup(_ => _.GetInstance(It.IsAny<MenuStateType>())).Returns(mockMenuState.Object);
            var gameMenu = new GameMenu(mockMenuStateFactory.Object);

            gameMenu.Perform(Command.BOOT);
            gameMenu.GetScreen();

            mockMenuState.Verify(_ => _.GetScreen(), Times.Once);
        }
    }
}