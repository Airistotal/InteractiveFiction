using InteractiveFiction.Business.Procedure;
using InteractiveFiction.ConsoleGame.Sanitize.Argument;

namespace InteractiveFiction.ConsoleGame.Tests.Sanitize.Argument
{
    public class ArgParserFactoryTests
    {
        [Theory]
        [InlineData(ProcedureType.Move, typeof(MoveArgParser))]
        [InlineData(ProcedureType.Look, typeof(LookArgParser))]
        [InlineData(ProcedureType.Attack, typeof(AttackArgParser))]
        [InlineData(ProcedureType.NULL, typeof(NullArgParser))]
        public void When_CreateParser_ReturnsCorrectOne(ProcedureType cmd, Type returnType)
        {
            var sut = new ArgParserFactory();

            var parser = sut.Create(cmd);

            Assert.IsType(returnType, parser);
        }
    }
}
