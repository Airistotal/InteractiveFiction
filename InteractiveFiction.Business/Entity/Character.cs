using InteractiveFiction.Business.Procedure;

namespace InteractiveFiction.Business.Entity
{
    public class Character : AnimateEntity
    {
        public Character(IProcedureBuilder? procedureBuilder) : base(procedureBuilder)
        {
        }
    }
}
