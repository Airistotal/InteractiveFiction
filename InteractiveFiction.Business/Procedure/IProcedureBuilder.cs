using InteractiveFiction.Business.Entity;

namespace InteractiveFiction.Business.Procedure
{
    public interface IProcedureBuilder
    {
        IProcedureBuilder agent(IEntity agent);
        IProcedure build();
        IProcedureBuilder target(IEntity target);
        IProcedureBuilder type(ProcedureType type);
    }
}