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
            if (color == System.Drawing.Color.Black)
            {
                return AnsiEscape.BLACK;
            }
            else if (color == System.Drawing.Color.Red)
            {
                return AnsiEscape.RED;
            } 
            else if (color == System.Drawing.Color.Blue)
            {
                return AnsiEscape.BLUE;
            }
            else if (color == System.Drawing.Color.Yellow)
            {
                return AnsiEscape.YELLOW;
            }
            else if (color == System.Drawing.Color.Green)
            {
                return AnsiEscape.GREEN;
            }
            else if (color == System.Drawing.Color.Purple)
            {
                return AnsiEscape.PURPLE;
            }
            else if (color == System.Drawing.Color.LightBlue)
            {
                return AnsiEscape.LIGHTBLUE;
            }
            else if (color == System.Drawing.Color.Gray)
            {
                return AnsiEscape.GREY;
            }
            else if (color == System.Drawing.Color.Salmon)
            {
                return AnsiEscape.SALMON;
            }
            else if (color == System.Drawing.Color.LightGreen)
            {
                return AnsiEscape.LIGHTGREEN;
            }
            else if (color == System.Drawing.Color.LightYellow)
            {
                return AnsiEscape.LIGHTYELLOW;
            }
            else if (color == System.Drawing.Color.MediumBlue)
            {
                return AnsiEscape.MEDBLUE;
            }
            else if (color == System.Drawing.Color.RebeccaPurple)
            {
                return AnsiEscape.ROYALPURPLE;
            }
            else if (color == System.Drawing.Color.SkyBlue)
            {
                return AnsiEscape.SKYBLUE;
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
            else if (color == System.Drawing.Color.Yellow)
            {
                return AnsiEscape.YELLOWHIGHLIGHT;
            }
            else if (color == System.Drawing.Color.Green)
            {
                return AnsiEscape.GREENHIGHLIGHT;
            }
            else if (color == System.Drawing.Color.Purple)
            {
                return AnsiEscape.PURPLEHIGHLIGHT;
            }
            else if (color == System.Drawing.Color.LightBlue)
            {
                return AnsiEscape.LIGHTBLUEHIGHLIGHT;
            }
            else if (color == System.Drawing.Color.Gray)
            {
                return AnsiEscape.GREYHIGHLIGHT;
            }
            else if (color == System.Drawing.Color.Salmon)
            {
                return AnsiEscape.SALMONHIGHLIGHT;
            }
            else if (color == System.Drawing.Color.LightGreen)
            {
                return AnsiEscape.LIGHTGREENHIGHLIGHT;
            }
            else if (color == System.Drawing.Color.LightYellow)
            {
                return AnsiEscape.LIGHTYELLOWHIGHLIGHT;
            }
            else if (color == System.Drawing.Color.MediumBlue)
            {
                return AnsiEscape.MEDBLUEHIGHLIGHT;
            }
            else if (color == System.Drawing.Color.RebeccaPurple)
            {
                return AnsiEscape.ROYALPURPLEHIGHLIGHT;
            }
            else if (color == System.Drawing.Color.SkyBlue)
            {
                return AnsiEscape.SKYBLUEHIGHLIGHT;
            }

            return "";
        }
    }
}
