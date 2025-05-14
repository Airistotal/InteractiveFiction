using InteractiveFiction.Business.Goal;
using InteractiveFiction.Business.Infrastructure;
using InteractiveFiction.Business.Procedure;
using System.IO.Abstractions;

namespace InteractiveFiction.Business.Entity
{
    public class EntityBuilderFactory : IEntityBuilderFactory
    {
        private readonly IObserverFactory trackerFactory;
        private readonly IProcedureBuilder procedureBuilder;
        private readonly ITextDecorator textDecorator;
        private readonly IFileSystem fileSystem;

        public EntityBuilderFactory(
            IObserverFactory trackerFactory,
            IProcedureBuilder procedureBuilder, 
            ITextDecorator textDecorator, 
            IFileSystem fileSystem)
        {
            this.trackerFactory = trackerFactory;
            this.procedureBuilder = procedureBuilder;
            this.textDecorator = textDecorator;
            this.fileSystem = fileSystem;

        }

        public IEntityBuilder GetBuilder()
        {
            return new EntityBuilder(trackerFactory, procedureBuilder, textDecorator, fileSystem);
        }
    }
}
