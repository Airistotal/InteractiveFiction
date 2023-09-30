using InteractiveFiction.Business.Entity;

namespace InteractiveFiction.Business.Procedure
{
    public interface IProcedureBuilder
    {
        IProcedureBuilder Agent(IAgent agent);
        IProcedure Build();
        IProcedureBuilder Type(ProcedureType type);
    }
}