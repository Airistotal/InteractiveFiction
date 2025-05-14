using InteractiveFiction.Business.Infrastructure;
using System.Drawing;

namespace InteractiveFiction.WorldBuilder.Business
{
    public class HtmlTextDecorator : ITextDecorator
    {
        public string Bold(string text)
        {
            throw new NotImplementedException();
        }

        public string Color(Color color, string text, bool isHighlight = false)
        {
            var background = isHighlight ? "background-" : "";
            return $"<span style=\"{background}color:#{color.R:X2}{color.G:X2}{color.B:X2}\">" + text + "</span>"; 
        }

        public string Underline(string text)
        {
            throw new NotImplementedException();
        }
    }
}
