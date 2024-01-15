using InteractiveFiction.WorldBuilder.Business;
using System.Drawing;

namespace InteractiveFiction.WorldBuilder.Tests.Business
{
    public class HtmlTextDecoratorTests
    {
        [Theory]
        [MemberData(nameof(testColors))]
        public void ColorAddsHexStyle(Color color)
        {
            var htmlTextDecorator = new HtmlTextDecorator();

            var decorated = htmlTextDecorator.Color(color, "");

            Assert.Contains($"style=\"color:#{color.R:X2}{color.G:X2}{color.B:X2}\"", decorated);
        }

        [Theory]
        [MemberData(nameof(testColors))]
        public void ColorHighlightAddsHexStyle(Color color)
        {
            var htmlTextDecorator = new HtmlTextDecorator();

            var decorated = htmlTextDecorator.Color(color, "", true);

            Assert.Contains($"style=\"background-color:#{color.R:X2}{color.G:X2}{color.B:X2}\"", decorated);
        }

        public static IEnumerable<object[]> testColors =>
            new List<object[]>
            {
                new object[] {Color.Red},
                new object[] {Color.Blue},
                new object[] {Color.Green},
                new object[] {Color.Yellow}
            };
    }
}