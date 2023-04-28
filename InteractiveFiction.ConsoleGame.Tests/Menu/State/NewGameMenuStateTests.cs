using InteractiveFiction.Business.Infrastructure;
using InteractiveFiction.Business.Infrastructure.MessageBus;
using InteractiveFiction.Business.Infrastructure.MessageBus.Message;
using InteractiveFiction.ConsoleGame.Menu;
using InteractiveFiction.ConsoleGame.Menu.State;
using Moq;

namespace InteractiveFiction.ConsoleGame.Tests.Menu.State
{
    public class NewGameMenuStateTests
    {
        [Fact]
        public void When_GetNewGameMenuStateScreen_ContainsGameArchetypes()
        {
            var sut = GetNewGameMenuState();

            Assert.Contains("1) Game 1", sut.GetScreen());
        }

        [Fact]
        public void When_NewGameMenuAction1_TransitionsToNewGameNameState()
        {
            var menuStateFactory = new Mock<IMenuStateFactory>();
            var sut = GetNewGameMenuState(menuStateFactory);

            var transition = sut.Transition(Command.ACTION1);

            menuStateFactory.Verify(_ => _.GetInstance(MenuStateType.NewGameName), Times.Once);
        }

        [Fact]
        public void When_NewGameMenuAction1_SendsSelectionEvent()
        {
            var messageBus = new Mock<IMessageBus>();
            var sut = GetNewGameMenuState(messageBus: messageBus);

            var transition = sut.Transition(Command.ACTION1);

            messageBus.Verify(_ => _.Publish(
                It.Is<GameArchetypeSelected>(_ => _.GameArchetype.Name.Equals("Game 1"))), Times.Once); 
        }

        private static NewGameMenuState GetNewGameMenuState(
            Mock<IMenuStateFactory>? menuStateFactory = null,
            Mock<IMessageBus>? messageBus = null)
        {
            if (menuStateFactory == null)
            {
                menuStateFactory = new Mock<IMenuStateFactory>();
            }

            var gameArchetypeLoader = new Mock<IGameArchetypeLoader>();
            gameArchetypeLoader.Setup(_ => _.LoadGameArchetypes())
                .Returns(new List<GameArchetype> { new GameArchetype() { Name = "Game 1" } });

            var textLoader = new Mock<ITextLoader>();
            textLoader.Setup(_ => _.GetText("menu:new_game:option_template")).Returns("{0}) {1}");

            if (messageBus == null)
            {
                messageBus = new Mock<IMessageBus>();
            }

            return new NewGameMenuState(menuStateFactory.Object, messageBus.Object, textLoader.Object, gameArchetypeLoader.Object);
        }
    }
}
 