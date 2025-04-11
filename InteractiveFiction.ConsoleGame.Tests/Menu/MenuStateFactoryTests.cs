using InteractiveFiction.Business.Infrastructure;
using InteractiveFiction.Business.Infrastructure.Game;
using InteractiveFiction.Business.Infrastructure.MessageBus;
using InteractiveFiction.ConsoleGame.Menu;
using InteractiveFiction.ConsoleGame.Menu.State;
using Moq;

namespace InteractiveFiction.ConsoleGame.Tests.Menu
{
    public class MenuStateFactoryTests
    {
        [Theory]
        [InlineData(MenuStateType.MainMenu, typeof(MainMenuState))]
        [InlineData(MenuStateType.NewGame, typeof(NewGameMenuState))]
        [InlineData(MenuStateType.NewGameName, typeof(NewGameNameMenuState))]
        [InlineData(MenuStateType.LoadGame, typeof(LoadGameMenuState))]
        [InlineData(MenuStateType.CharacterSelect, typeof(CharacterSelectMenuState))]
        [InlineData(MenuStateType.CreateCharacter, typeof(CreateCharacterMenuState))]
        public void When_RequestMainMenu_CreatesMainMenu(MenuStateType type, Type instanceType)
        {
            var sut = new MenuStateFactory(
                new Mock<ITextLoader>().Object,
                new Mock<IGameArchetypeLoader>().Object,
                new Mock<IMessageBus>().Object);

            var menuState = sut.GetInstance(type);

            Assert.IsType(instanceType, menuState);
        }
    }
}
