namespace InteractiveFiction.Business.Exceptions
{
    public class NullAgentException : Exception
    {
        public NullAgentException(string? message) : base(message)
        {
        }
    }
}
