using InteractiveFiction.Business.Infrastructure;
using InteractiveFiction.Business.Infrastructure.MessageBus;
using InteractiveFiction.Business.Infrastructure.MessageBus.Message;
using InteractiveFiction.ConsoleGame.Menu;
using InteractiveFiction.ConsoleGame.Menu.State;
using Moq;

namespace InteractiveFiction.ConsoleGame.Tests.Menu.State
{
    public class NewGameNameMenuStateTests
    {
        [Fact]
        public void When_GetNewGameNameMenuStateScreen_PromptsForName()
        {
            var sut = GetNewGameNameMenuState();

            Assert.Contains("Enter game name:", sut.GetScreen());
        }

        [Fact]
        public void When_NewGameNameAnyAction_NoName_ReturnsSelf()
        {
            var sut = GetNewGameNameMenuState();

            var transition = sut.Transition(Command.ACTION1);

            Assert.Equal(sut, transition);
        }

        [Fact]
        public void When_NewGameNameAnyAction_EmptyName_ReturnsSelf()
        {
            var sut = GetNewGameNameMenuState();

            var transition = sut.Transition(Command.ACTION1, "");

            Assert.Equal(sut, transition);
        }

        [Fact]
        public void When_NewGameNameAnyAction_TransitionsToCreateCharacterState()
        {
            var menuStateFactory = new Mock<IMenuStateFactory>();
            var sut = GetNewGameNameMenuState(menuStateFactory);

            var transition = sut.Transition(Command.ACTION1, "Game 1");

            menuStateFactory.Verify(_ => _.GetInstance(MenuStateType.CreateCharacter), Times.Once);
        }

        [Fact]
        public void When_NewGameNameAnyAction_SendsSelectionEvent()
        {
            var messageBus = new Mock<IMessageBus>();
            var sut = GetNewGameNameMenuState(messageBus: messageBus);

            var transition = sut.Transition(Command.ACTION1, "Game 1");

            messageBus.Verify(_ => _.Publish(
                It.Is<GameNameSelected>(_ => _.Name.Equals("Game 1"))), Times.Once);
        }

        private static NewGameNameMenuState GetNewGameNameMenuState(
            Mock<IMenuStateFactory>? menuStateFactory = null,
            Mock<IMessageBus>? messageBus = null)
        {
            if (menuStateFactory == null)
            {
                menuStateFactory = new Mock<IMenuStateFactory>();
            }

            var textLoader = new Mock<ITextLoader>();
            textLoader.Setup(_ => _.GetText("menu.new_game.game_name_prompt")).Returns("Enter game name:");

            if (messageBus == null)
            {
                messageBus = new Mock<IMessageBus>();
            }

            return new NewGameNameMenuState(menuStateFactory.Object, messageBus.Object, textLoader.Object);
        }
    }
}
