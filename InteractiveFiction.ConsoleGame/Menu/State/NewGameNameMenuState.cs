using InteractiveFiction.Business.Infrastructure;
using InteractiveFiction.Business.Infrastructure.MessageBus;
using InteractiveFiction.Business.Infrastructure.MessageBus.Message;

namespace InteractiveFiction.ConsoleGame.Menu.State
{
    public class NewGameNameMenuState : IMenuState
    {
        private IMessageBus messageBus;
        private ITextLoader textLoader;
        private IMenuStateFactory menuStateFactory;

        public NewGameNameMenuState(IMenuStateFactory menuStateFactory, IMessageBus messageBus, ITextLoader textLoader)
        {
            this.messageBus = messageBus;
            this.textLoader = textLoader;
            this.menuStateFactory = menuStateFactory;
        }

        public string GetScreen()
        {
            return textLoader.GetText("menu.new_game.game_name_prompt");
        }

        public IMenuState Transition(Command command, params string[] values)
        {
            if (values == null || values.Length == 0 || string.IsNullOrWhiteSpace(values[0])) {
                return this;
            }

            messageBus.Publish(new GameNameSelected() { Name = values[0] });

            return menuStateFactory.GetInstance(MenuStateType.CreateCharacter);
        }
    }
}
