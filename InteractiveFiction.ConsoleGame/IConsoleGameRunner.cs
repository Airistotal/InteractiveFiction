namespace InteractiveFiction.ConsoleGame
{
    public interface IConsoleGameRunner
    {
        string GetScreen();
        void Perform(string input);
        void Tick();
    }
}