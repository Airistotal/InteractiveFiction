using InteractiveFiction.Business.Infrastructure;
using InteractiveFiction.ConsoleGame.Menu;
using InteractiveFiction.ConsoleGame.Menu.State;
using Moq;

namespace InteractiveFiction.ConsoleGame.Tests.Menu.State
{
    public class MainMenuStateTests
    {
        [Fact]
        public void When_GetMainMenuScreen_ReturnsIdleText()
        {
            var textLoader = new Mock<ITextLoader>();
            textLoader.Setup(_ => _.GetText("menu:idle")).Returns("Main Menu");
            var sut = new MainMenuState(new Mock<IMenuStateFactory>().Object, textLoader.Object);

            Assert.Equal("Main Menu", sut.GetScreen());
            textLoader.Verify(_ => _.GetText("menu:idle"), Times.Once);
        }

        [Fact]
        public void When_MainMenuActionOne_TransitionsToNewGame()
        {
            var menuStateFactory = new Mock<IMenuStateFactory>();
            var sut = new MainMenuState(menuStateFactory.Object, new Mock<ITextLoader>().Object);

            var menuState = sut.Transition(Command.ACTION1);

            menuStateFactory.Verify(_ => _.GetInstance(MenuStateType.NewGame), Times.Once);
        }

        [Fact]
        public void When_MainMenuActionTwo_TransitionsToLoadGame()
        {
            var menuStateFactory = new Mock<IMenuStateFactory>();
            var sut = new MainMenuState(menuStateFactory.Object, new Mock<ITextLoader>().Object);

            var menuState = sut.Transition(Command.ACTION2);

            menuStateFactory.Verify(_ => _.GetInstance(MenuStateType.LoadGame), Times.Once);
        }
    }
}
