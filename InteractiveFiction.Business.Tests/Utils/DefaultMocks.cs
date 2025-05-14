using InteractiveFiction.Business.Entity;
using InteractiveFiction.Business.Entity.AnimateEntities;
using InteractiveFiction.Business.Entity.Locations;
using InteractiveFiction.Business.Infrastructure;
using InteractiveFiction.Business.Procedure;
using Moq;
using System.Drawing;

namespace InteractiveFiction.Business.Tests.Utils
{
    internal class DefaultMocks
    {
        public static Mock<IProcedureBuilder> GetProcedureBuilderMock()
        {
            var procedureBuilder = new Mock<IProcedureBuilder>();

            procedureBuilder.Setup(_ => _.Type(It.IsAny<ProcedureType>())).Returns(procedureBuilder.Object);
            procedureBuilder.Setup(_ => _.Agent(It.IsAny<IAgent>())).Returns(procedureBuilder.Object);
            procedureBuilder.Setup(_ => _.Build()).Returns(new Mock<IProcedure>().Object);

            return procedureBuilder;
        }

        public static Mock<IEntityBuilderFactory> GetEntityBuilderFactoryMock(Mock<IEntityBuilder>? builder = null)
        {
            if (builder == null)
            {
                builder = new Mock<IEntityBuilder>();
                builder.Setup(_ => _.From(It.IsAny<string>())).Returns(builder.Object);
                builder.Setup(_ => _.Build()).Returns(new Mock<IEntity>().Object);
            }

            var entityBuilderFactory = new Mock<IEntityBuilderFactory>();
            entityBuilderFactory.Setup(_ => _.GetBuilder()).Returns(builder.Object);

            return entityBuilderFactory;
        }
    
        public static Mock<ITextDecorator> GetTextDecorator()
        {
            var decorator = new Mock<ITextDecorator>();
            decorator.Setup(_ => _.Underline(It.IsAny<string>())).Returns((string x) => x);
            decorator.Setup(_ => _.Bold(It.IsAny<string>())).Returns((string x) => x);
            decorator.Setup(_ => _.Color(It.IsAny<Color>(), It.IsAny<string>(), It.IsAny<bool>())).Returns((Color x, string y, bool z) => y);

            return decorator;
        }
    }
}
