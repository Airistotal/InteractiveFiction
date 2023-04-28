using InteractiveFiction.ConsoleGame.Menu.State;

namespace InteractiveFiction.ConsoleGame.Menu
{
    public interface IMenuStateFactory
    {
        IMenuState GetInstance(MenuStateType type);
    }
}
