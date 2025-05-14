using InteractiveFiction.Business.Infrastructure.MessageBus;
using InteractiveFiction.Business.Infrastructure.MessageBus.Message;
using InteractiveFiction.ConsoleGame.Menu;

namespace InteractiveFiction.ConsoleGame
{
    public class ConsoleGameRunner : IConsoleGameRunner
    {
        private readonly IGameMenu gameMenu;
        private readonly IGameContainer gameContainer;
        private readonly IMessageBus messageBus;

        private bool isInGame = false;

        public ConsoleGameRunner(
            IMessageBus messageBus,
            IGameMenu gameMenu,
            IGameContainer gameContainer)
        {
            this.gameMenu = gameMenu;
            this.gameContainer = gameContainer;
            this.messageBus = messageBus;

            this.messageBus.Register<MoveToGameMessage>(this.HandleMoveToGame);

            Perform("boot");
        }

        public string GetScreen()
        {
            if (!isInGame)
            {
                return this.gameMenu.GetScreen();
            } else
            {
                return this.gameContainer.GetScreen();
            }
        }

        public void Perform(string input)
        {
            if (!isInGame)
            {
                this.gameMenu.Perform(input);
            } else
            {
                this.gameContainer.Perform(input);
            }
        }

        public void Tick()
        {
            if (isInGame)
            {
                this.gameContainer.Tick();
            }
        }

        public void HandleMoveToGame(IMessage message)
        {
            if (message is MoveToGameMessage moveToGame)
            {
                if (gameContainer.IsReady())
                {
                    isInGame = true;
                } else
                {
                    throw new CantStartGameException();
                }
            }
        }
    }
}
