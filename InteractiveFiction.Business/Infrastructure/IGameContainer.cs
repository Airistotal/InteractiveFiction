using InteractiveFiction.Business.Procedure;

namespace InteractiveFiction.ConsoleGame
{
    public interface IGameContainer
    {
        bool IsReady();
        string GetScreen();
        void Perform(ProcedureCommand command);
        void Tick();
    }
}
