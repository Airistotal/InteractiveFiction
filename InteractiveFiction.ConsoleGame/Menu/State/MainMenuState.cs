using InteractiveFiction.Business.Infrastructure;

namespace InteractiveFiction.ConsoleGame.Menu.State
{
    public class MainMenuState : IMenuState
    {
        private readonly IMenuStateFactory menuStateFactory;
        private readonly ITextLoader textLoader;

        public MainMenuState(IMenuStateFactory menuStateFactory, ITextLoader textLoader)
        {
            this.menuStateFactory = menuStateFactory;
            this.textLoader = textLoader;
        }

        public string GetScreen()
        {
            return textLoader.GetText("menu.idle");
        }

        public IMenuState Transition(Command command, params string[] values)
        {
            switch (command)
            {
                case Command.ACTION1:
                    return menuStateFactory.GetInstance(MenuStateType.NewGame);
                case Command.ACTION2:
                    return menuStateFactory.GetInstance(MenuStateType.LoadGame);
                default:
                    return this;
            }
        }
    }
}
