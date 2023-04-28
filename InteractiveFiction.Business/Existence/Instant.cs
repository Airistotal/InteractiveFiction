using InteractiveFiction.Business.Entity;

namespace InteractiveFiction.Business.Existence
{
    public class Instant
    {
        public IEntity Root { get; }

        public Instant(IEntity root)
        {
            Root = root;
        }
    }
}
