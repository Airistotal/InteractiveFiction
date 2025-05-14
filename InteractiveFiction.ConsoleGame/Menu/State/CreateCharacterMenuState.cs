using InteractiveFiction.Business.Infrastructure;
using InteractiveFiction.Business.Infrastructure.MessageBus;
using InteractiveFiction.Business.Infrastructure.MessageBus.Message;

namespace InteractiveFiction.ConsoleGame.Menu.State
{
    public class CreateCharacterMenuState : IMenuState
    {
        private readonly IMessageBus messageBus;
        private readonly ITextLoader textLoader;
        private readonly IMenuStateFactory menuStateFactory;

        public CreateCharacterMenuState(IMenuStateFactory menuStateFactory, IMessageBus messageBus, ITextLoader textLoader)
        {
            this.menuStateFactory = menuStateFactory;
            this.messageBus = messageBus;
            this.textLoader = textLoader;
        }

        public string GetScreen()
        {
            return textLoader.GetText("menu.new_character.character_name_prompt");
        }

        public IMenuState Transition(Command command, params string[] values)
        {
            if (values == null || values.Length == 0 || string.IsNullOrWhiteSpace(values[0]))
            {
                return this;
            }

            messageBus.Publish(new CharacterInfoSelected() { Name = values[0] });

            return menuStateFactory.GetInstance(MenuStateType.MainMenu);
        }
    }
}
