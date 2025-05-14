using InteractiveFiction.Business.Entity;
using InteractiveFiction.Business.Procedure;

namespace InteractiveFiction.Business.Existence
{
    public interface IUniverse
    {
        Instant GetInstant();
        void Put(IProcedure procedure);
        void RegisterLaw(ILaw law);
        void Tick();
        void Spawn(IEntity entity);
    }
}