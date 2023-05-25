using InteractiveFiction.Business.Entity;

namespace InteractiveFiction.Business.Procedure
{
    public interface IProcedureBuilder
    {
        IProcedureBuilder agent(IEntity agent);
        IProcedure build();
        IProcedureBuilder type(ProcedureType type);
    }
}