using InteractiveFiction.Business.Entity;

namespace InteractiveFiction.Business.Procedure
{
    public abstract class BaseProcedure
    {
        public IEntity? Agent { get; set; }
        public IEntity? Target { get; set; }
    }
}
