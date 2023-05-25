using System.Drawing;

namespace InteractiveFiction.ConsoleGame.Tests
{
    public class ConsoleTextDecoratorTests
    {
        public static IEnumerable<object[]> ColorMatches => new List<object[]>
        {
            new object[] {Color.Red, AnsiEscape.RED, false},
            new object[] {Color.Red, AnsiEscape.REDHIGHLIGHT, true}
        };

        [Theory]
        [MemberData(nameof(ColorMatches))]
        public void When_Colour_UsesAnsiColor(Color color, string ansiColor, bool highlight)
        {
            var sut = new ConsoleTextDecorator();

            var response = sut.Color(color, "test", highlight);

            Assert.Contains(ansiColor, response);
            Assert.Contains(AnsiEscape.RESET, response);
        }

        [Fact]
        public void When_EncodeBold_UsesAnsiBold()
        {
            var sut = new ConsoleTextDecorator();

            var bolded = sut.Bold("bold");

            Assert.Contains(AnsiEscape.BOLD, bolded);
            Assert.Contains(AnsiEscape.RESET, bolded);
        }

        [Fact]
        public void When_Underline_UsesAnsiUnderline()
        {
            var sut = new ConsoleTextDecorator();

            var bolded = sut.Underline("underline");

            Assert.Contains(AnsiEscape.UNDERLINE, bolded);
            Assert.Contains(AnsiEscape.RESET, bolded);

        }
    }
}
