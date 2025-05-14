using InteractiveFiction.Business.Infrastructure;
using InteractiveFiction.Business.Procedure;
using System.IO.Abstractions;

namespace InteractiveFiction.Business.Entity
{
    public class EntityBuilderFactory : IEntityBuilderFactory
    {
        private readonly IProcedureBuilder procedureBuilder;
        private readonly ITextDecorator textDecorator;
        private readonly IFileSystem fileSystem;

        public EntityBuilderFactory(IProcedureBuilder procedureBuilder, ITextDecorator textDecorator, IFileSystem fileSystem)
        {
            this.procedureBuilder = procedureBuilder;
            this.textDecorator = textDecorator;
            this.fileSystem = fileSystem;

        }

        public IEntityBuilder GetBuilder()
        {
            return new EntityBuilder(procedureBuilder, textDecorator, fileSystem);
        }
    }
}
