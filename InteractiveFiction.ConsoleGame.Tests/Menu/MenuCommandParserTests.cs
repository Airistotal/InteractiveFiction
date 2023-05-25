using InteractiveFiction.ConsoleGame.Menu;

namespace InteractiveFiction.ConsoleGame.Tests.Menu
{
    public class MenuCommandParserTests
    {
        [Theory]
        [InlineData("boot", Command.BOOT)]
        [InlineData("1", Command.ACTION1)]
        public void When_ParseInput_ReturnsCommand(string input, Command command)
        {
            var sut = new MenuCommandParser();

            var result = sut.Parse(input);

            Assert.Equal(command, result);
        }
    }
}
