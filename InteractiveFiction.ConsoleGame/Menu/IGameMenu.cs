namespace InteractiveFiction.ConsoleGame.Menu
{
    public interface IGameMenu
    {
        string GetScreen();
        void Perform(Command command);
    }
}