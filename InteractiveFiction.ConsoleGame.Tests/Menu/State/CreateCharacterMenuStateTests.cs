using InteractiveFiction.Business.Infrastructure;
using InteractiveFiction.Business.Infrastructure.MessageBus;
using InteractiveFiction.Business.Infrastructure.MessageBus.Message;
using InteractiveFiction.ConsoleGame.Menu;
using InteractiveFiction.ConsoleGame.Menu.State;
using Moq;

namespace InteractiveFiction.ConsoleGame.Tests.Menu.State
{
    public class CreateCharacterMenuStateTests
    {
        [Fact]
        public void When_GetCreateCharacterScreen_PromptsForName()
        {
            var sut = GetCreateCharacterMenuState();

            Assert.Contains("Enter character name:", sut.GetScreen());
        }

        [Fact]
        public void When_CreateCharacterAction1_NoName_ReturnsSelf()
        {
            var sut = GetCreateCharacterMenuState();

            var transition = sut.Transition(Command.ACTION1);

            Assert.Equal(sut, transition);
        }

        [Fact]
        public void When_CreateCharacterAction1_EmptyName_ReturnsSelf()
        {
            var sut = GetCreateCharacterMenuState();

            var transition = sut.Transition(Command.ACTION1, "");

            Assert.Equal(sut, transition);
        }

        [Fact]
        public void When_CreateCharacterAction1_TransitionsToMainMenu()
        {
            var menuStateFactory = new Mock<IMenuStateFactory>();
            var sut = GetCreateCharacterMenuState(menuStateFactory);

            var transition = sut.Transition(Command.ACTION1, "Character 1");

            menuStateFactory.Verify(_ => _.GetInstance(MenuStateType.MainMenu), Times.Once);
        }

        [Fact]
        public void When_CreateCharacterAction1_SendsSelectionEvent()
        {
            var messageBus = new Mock<IMessageBus>();
            var sut = GetCreateCharacterMenuState(messageBus: messageBus);

            var transition = sut.Transition(Command.ACTION1, "Character 1");

            messageBus.Verify(_ => _.Publish(
                It.Is<CharacterNameSelected>(_ => _.Name.Equals("Character 1"))), Times.Once);
        }

        private static CreateCharacterMenuState GetCreateCharacterMenuState(
            Mock<IMenuStateFactory>? menuStateFactory = null,
            Mock<IMessageBus>? messageBus = null)
        {
            if (menuStateFactory == null)
            {
                menuStateFactory = new Mock<IMenuStateFactory>();
            }

            var textLoader = new Mock<ITextLoader>();
            textLoader.Setup(_ => _.GetText("menu:new_character:character_name_prompt")).Returns("Enter character name:");

            if (messageBus == null)
            {
                messageBus = new Mock<IMessageBus>();
            }

            return new CreateCharacterMenuState(menuStateFactory.Object, messageBus.Object, textLoader.Object);
        }
    }
}
