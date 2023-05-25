using InteractiveFiction.Business.Infrastructure;
using InteractiveFiction.Business.Infrastructure.MessageBus;
using InteractiveFiction.Business.Infrastructure.MessageBus.Message;

namespace InteractiveFiction.ConsoleGame.Menu.State
{
    public class NewGameMenuState : IMenuState
    {
        private IMessageBus messageBus;
        private ITextLoader textLoader;
        private IMenuStateFactory menuStateFactory;

        private List<GameArchetype> gameArchetypes;

        public NewGameMenuState(IMenuStateFactory menuStateFactory, IMessageBus messageBus, ITextLoader textLoader, IGameArchetypeLoader gameArchetypeLoader)
        {
            this.messageBus = messageBus;
            this.menuStateFactory = menuStateFactory;
            this.textLoader = textLoader;

            gameArchetypes = gameArchetypeLoader.LoadGameArchetypes();
        }

        public string GetScreen()
        {
            var screen = textLoader.GetText("menu.new_game.intro");
            var optionTemplate = textLoader.GetText("menu.new_game.option_template");

            for (int i = 0; i < gameArchetypes.Count; i++)
            {
                var gameArchetype = gameArchetypes[i];

                screen += Environment.NewLine + string.Format(optionTemplate, i+1, gameArchetype.Name);
            }

            return screen;
        }

        public IMenuState Transition(Command command, params string[] values)
        {
            switch (command)
            {
                case Command.ACTION1:
                    messageBus.Publish(new GameArchetypeSelected(gameArchetypes[0]));

                    return menuStateFactory.GetInstance(MenuStateType.NewGameName);
                default:
                    return this;
            }
        }
    }
}
