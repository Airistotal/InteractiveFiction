using System.Runtime.Serialization;

namespace InteractiveFiction.Business.Infrastructure
{
    [Serializable]
    public class NoGameException : Exception
    {
        public NoGameException()
        {
        }

        public NoGameException(string? message) : base(message)
        {
        }

        public NoGameException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NoGameException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}