using InteractiveFiction.Business.Existence;
using InteractiveFiction.Business.Procedure;
using InteractiveFiction.Business.Procedure.Argument;

namespace InteractiveFiction.Business.Entity
{
    public interface IEntity
    {
        void Perform(ProcedureType type, List<IProcedureArg> args);
        void ArchiveEvents();
        void AddEvent(string evt);
        void SetLocation(Location location);
        void SetUniverse(IUniverse universe);
        bool Is(string id);
        Location GetLocation();
        List<string> GetNewEvents();
        string GetFullDescription();
    }
}
