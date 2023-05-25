using System.Drawing;

namespace InteractiveFiction.Business.Infrastructure
{
    public interface ITextDecorator
    {
        public string Bold(string text);
        public string Underline(string text);
        public string Color(Color color, string text, bool isHighlight = false);
    }
}
