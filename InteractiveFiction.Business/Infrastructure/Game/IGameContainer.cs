using InteractiveFiction.Business.Procedure;

namespace InteractiveFiction.Business.Infrastructure.Game
{
    public interface IGameContainer
    {
        bool IsReady();
        string GetScreen();
        SaveData GetSaveData();
        void Load(SaveData data);
        void Perform(ProcedureCommand command);
        void Tick();
    }
}
