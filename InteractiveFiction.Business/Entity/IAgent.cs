using InteractiveFiction.Business.Procedure;

namespace InteractiveFiction.Business.Entity
{
    public interface IAgent
    {
        void Perform(ProcedureType type, List<IProcedureArg> args);
        void ArchiveEvents();
        void AddEvent(string evt);
        List<string> GetNewEvents();
    }
}
