using InteractiveFiction.Business.Procedure;

namespace InteractiveFiction.Business.Entity
{
    public interface IEntity
    {
        void Perform(ProcedureType type, string[] args);
    }
}
