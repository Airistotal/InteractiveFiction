using InteractiveFiction.Business.Infrastructure;
using System.Drawing;

namespace InteractiveFiction.ConsoleGame
{
    public class ConsoleTextDecorator : ITextDecorator
    {
        public string Bold(string text)
        {
            return $"{AnsiEscape.BOLD}{text}{AnsiEscape.RESET}";
        }

        public string Color(Color color, string text, bool isHighlight = false)
        {
            var escape = isHighlight ? GetAnsiHighlight(color) : GetAnsiColor(color);

            return $"{escape}{text}{AnsiEscape.RESET}";
        }

        public string Underline(string text)
        {
            return $"{AnsiEscape.UNDERLINE}{text}{AnsiEscape.RESET}";
        }

        private static string GetAnsiColor(Color color)
        {
            if (color == System.Drawing.Color.Red)
            {
                return AnsiEscape.RED;
            } else if (color == System.Drawing.Color.Blue)
            {
                return AnsiEscape.BLUE;
            }

            return "";
        }

        private static string GetAnsiHighlight(Color color)
        {
            if (color == System.Drawing.Color.Red)
            {
                return AnsiEscape.REDHIGHLIGHT;
            }
            else if (color == System.Drawing.Color.Blue)
            {
                return AnsiEscape.BLUEHIGHLIGHT;
            }

            return "";
        }
    }
}
