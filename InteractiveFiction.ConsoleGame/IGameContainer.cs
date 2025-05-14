namespace InteractiveFiction.ConsoleGame
{
    public interface IGameContainer
    {
        bool IsReady();
        string GetScreen();
        void Perform(string input);
        void Tick();
    }
}
