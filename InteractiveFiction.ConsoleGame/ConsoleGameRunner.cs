using InteractiveFiction.Business.Existence;
using InteractiveFiction.Business.Infrastructure.MessageBus;
using InteractiveFiction.ConsoleGame.Menu;

namespace InteractiveFiction.ConsoleGame
{
    public class ConsoleGameRunner
    {
        private readonly IGameMenu gameMenu;
        private readonly IMessageBus messageBus;
        private readonly IUniverseLoader universeLoader;

        public ConsoleGameRunner(IGameMenu gameMenu, IMessageBus messageBus, IUniverseLoader universeLoader)
        {
            this.gameMenu = gameMenu;
            this.gameMenu.Perform(Command.BOOT);
        }

        public string GetScreen()
        {
            return this.gameMenu.GetScreen();
        }
    }
}
