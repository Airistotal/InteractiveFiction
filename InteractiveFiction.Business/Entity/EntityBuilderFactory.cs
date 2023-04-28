using InteractiveFiction.Business.Procedure;

namespace InteractiveFiction.Business.Entity
{
    public class EntityBuilderFactory : IEntityBuilderFactory
    {
        private IProcedureBuilder procedureBuilder;

        public EntityBuilderFactory(IProcedureBuilder procedureBuilder)
        {
            this.procedureBuilder = procedureBuilder;
        }

        public IEntityBuilder GetBuilder()
        {
            return new EntityBuilder(this.procedureBuilder);
        }
    }
}
