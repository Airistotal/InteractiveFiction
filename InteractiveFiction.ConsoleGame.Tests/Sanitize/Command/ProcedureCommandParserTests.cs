using InteractiveFiction.Business.Existence;
using InteractiveFiction.Business.Procedure;
using InteractiveFiction.Business.Procedure.Argument;
using InteractiveFiction.ConsoleGame.Sanitize.Argument;
using InteractiveFiction.ConsoleGame.Sanitize.Commands;
using Moq;

namespace InteractiveFiction.ConsoleGame.Tests.Sanitize.Commands
{
    public class ProcedureCommandParserTests
    {
        [Theory]
        [InlineData("m w", ProcedureType.Move)]
        [InlineData("move w", ProcedureType.Move)]
        [InlineData("moVE w", ProcedureType.Move)]
        [InlineData("l", ProcedureType.Look)]
        [InlineData("l w", ProcedureType.Look)]
        [InlineData("look", ProcedureType.Look)]
        [InlineData("lOoK", ProcedureType.Look)]
        public void When_ParseInput_ReturnsProcedureCommand(string input, ProcedureType type)
        {
            var arg = new Mock<IProcedureArg>().Object;
            var sut = new ProcedureCommandParser(CreateMockArgParserFactory(arg));

            var result = sut.Parse(input);

            Assert.Equal(type, result.ProcedureType);

            if (input.Split(" ").Length > 1)
            {
                Assert.Equal(arg, result.Args[0]);
            }
        }

        private IArgParserFactory CreateMockArgParserFactory(IProcedureArg arg)
        {
            var argParser = new Mock<IArgParser>();
            var argParserFactory = new Mock<IArgParserFactory>();
            argParserFactory.Setup(_ => _.Create(It.IsAny<ProcedureType>())).Returns(argParser.Object);
            argParser.Setup(_ => _.Parse(It.IsAny<List<string>>())).Returns(new List<IProcedureArg>() { arg });

            return argParserFactory.Object;
        }
    }
}
