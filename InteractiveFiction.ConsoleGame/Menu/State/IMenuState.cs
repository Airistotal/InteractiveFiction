namespace InteractiveFiction.ConsoleGame.Menu.State
{
    public interface IMenuState
    {
        IMenuState Transition(Command command, params string[] values);
        string GetScreen();
    }
}
