using InteractiveFiction.ConsoleGame.Menu.State;
using InteractiveFiction.ConsoleGame.Sanitize.Commands;

namespace InteractiveFiction.ConsoleGame.Menu
{
    public class GameMenu : IGameMenu
    {
        private IMenuState? menuState;

        private readonly IMenuStateFactory menuStateFactory;
        private readonly ICommandParser commandParser;

        public GameMenu(IMenuStateFactory menuStateFactory, ICommandParser commandParser)
        {
            this.menuStateFactory = menuStateFactory;
            this.commandParser = commandParser;
        }

        public void Perform(string input)
        {
            var command = this.commandParser.Parse(input);

            if (menuState == null && command == Command.BOOT)
            {
                menuState = menuStateFactory.GetInstance(MenuStateType.MainMenu);
            }
            else if (menuState != null)
            {
                menuState = menuState.Transition(command, input);
            }
        }

        public string GetScreen()
        {
            if (menuState == null) { return string.Empty; }

            return menuState.GetScreen();
        }
    }
}
