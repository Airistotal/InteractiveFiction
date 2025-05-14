using InteractiveFiction.ConsoleGame.Menu.State;

namespace InteractiveFiction.ConsoleGame.Menu
{
    public class GameMenu : IGameMenu
    {
        private IMenuState menuState;
        private readonly IMenuStateFactory menuStateFactory;

        public GameMenu(IMenuStateFactory menuStateFactory)
        {
            this.menuStateFactory = menuStateFactory;
        }

        public void Perform(Command command)
        {
            if (menuState == null && command == Command.BOOT)
            {
                menuState = menuStateFactory.GetInstance(MenuStateType.MainMenu);
            }
            else if (menuState != null)
            {
                menuState = menuState.Transition(command);
            }
        }

        public string GetScreen()
        {
            return menuState.GetScreen();
        }
    }
}
