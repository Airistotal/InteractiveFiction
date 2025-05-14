using InteractiveFiction.Business.Infrastructure;
using InteractiveFiction.Business.Procedure;

namespace InteractiveFiction.Business.Entity
{
    public class EntityBuilderFactory : IEntityBuilderFactory
    {
        private IProcedureBuilder procedureBuilder;
        private ITextDecorator textDecorator;

        public EntityBuilderFactory(IProcedureBuilder procedureBuilder, ITextDecorator textDecorator)
        {
            this.procedureBuilder = procedureBuilder;
            this.textDecorator = textDecorator;

        }

        public IEntityBuilder GetBuilder()
        {
            return new EntityBuilder(procedureBuilder, textDecorator);
        }
    }
}
