using InteractiveFiction.Business.Entity;
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

            procedureBuilder.Setup(_ => _.type(It.IsAny<ProcedureType>())).Returns(procedureBuilder.Object);
            procedureBuilder.Setup(_ => _.agent(It.IsAny<IEntity>())).Returns(procedureBuilder.Object);
            procedureBuilder.Setup(_ => _.build()).Returns(new Mock<IProcedure>().Object);

            return procedureBuilder;
        }

        public static Mock<IEntityBuilderFactory> GetEntityBuilderFactoryMock(Mock<IEntityBuilder>? locationBuilder = null)
        {
            if (locationBuilder == null)
            { 
                locationBuilder = new Mock<IEntityBuilder>();
                locationBuilder.Setup(_ => _.FromLines(It.IsAny<IEnumerable<string>>())).Returns(locationBuilder.Object);
                locationBuilder.Setup(_ => _.Build()).Returns(new Location(GetTextDecorator().Object));
            }

            var characterBuilder = new Mock<IEntityBuilder>();
            characterBuilder.Setup(_ => _.Build()).Returns(new Character(GetProcedureBuilderMock().Object, GetTextDecorator().Object));

            var entityBuilder = new Mock<IEntityBuilder>();
            entityBuilder.Setup(_ => _.FromLines(It.Is<IEnumerable<string>>(_ => _.Any(_ => _.StartsWith("Type:CHARACTER")))))
                .Returns(characterBuilder.Object);
            entityBuilder.Setup(_ => _.FromLines(It.Is<IEnumerable<string>>(_ => _.Any(_ => _.StartsWith("Type:LOCATION")))))
                .Returns((IEnumerable<string> x) => locationBuilder.Object.FromLines(x));


            var entityBuilderFactory = new Mock<IEntityBuilderFactory>();
            entityBuilderFactory.Setup(_ => _.GetBuilder()).Returns(entityBuilder.Object);

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
