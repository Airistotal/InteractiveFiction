using System.Drawing;

namespace InteractiveFiction.ConsoleGame.Tests
{
    public class ConsoleTextDecoratorTests
    {
        public static IEnumerable<object[]> ColorMatches => new List<object[]>
        {
            new object[] {Color.Black, AnsiEscape.BLACK, false},
            new object[] {Color.Red, AnsiEscape.RED, false},
            new object[] {Color.Red, AnsiEscape.REDHIGHLIGHT, true},
            new object[] {Color.Blue, AnsiEscape.BLUE, false},
            new object[] {Color.Blue, AnsiEscape.BLUEHIGHLIGHT, true},
            new object[] {Color.Yellow, AnsiEscape.YELLOW, false},
            new object[] {Color.Yellow, AnsiEscape.YELLOWHIGHLIGHT, true},
            new object[] {Color.Green, AnsiEscape.GREEN, false},
            new object[] {Color.Green, AnsiEscape.GREENHIGHLIGHT, true},
            new object[] {Color.Purple, AnsiEscape.PURPLE, false},
            new object[] {Color.Purple, AnsiEscape.PURPLEHIGHLIGHT, true},
            new object[] {Color.LightBlue, AnsiEscape.LIGHTBLUE, false},
            new object[] {Color.LightBlue, AnsiEscape.LIGHTBLUEHIGHLIGHT, true},
            new object[] {Color.Gray, AnsiEscape.GREY, false},
            new object[] {Color.Gray, AnsiEscape.GREYHIGHLIGHT, true},
            new object[] {Color.Salmon, AnsiEscape.SALMON, false},
            new object[] {Color.Salmon, AnsiEscape.SALMONHIGHLIGHT, true},
            new object[] {Color.LightGreen, AnsiEscape.LIGHTGREEN, false},
            new object[] {Color.LightGreen, AnsiEscape.LIGHTGREENHIGHLIGHT, true},
            new object[] {Color.LightYellow, AnsiEscape.LIGHTYELLOW, false},
            new object[] {Color.LightYellow, AnsiEscape.LIGHTYELLOWHIGHLIGHT, true},
            new object[] {Color.MediumBlue, AnsiEscape.MEDBLUE, false},
            new object[] {Color.MediumBlue, AnsiEscape.MEDBLUEHIGHLIGHT, true},
            new object[] {Color.RebeccaPurple, AnsiEscape.ROYALPURPLE, false},
            new object[] {Color.RebeccaPurple, AnsiEscape.ROYALPURPLEHIGHLIGHT, true},
            new object[] {Color.SkyBlue, AnsiEscape.SKYBLUE, false},
            new object[] {Color.SkyBlue, AnsiEscape.SKYBLUEHIGHLIGHT, true}
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
