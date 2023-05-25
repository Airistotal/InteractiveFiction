namespace InteractiveFiction.Business.Procedure
{
    public class NullAgentException : Exception
    {
        public NullAgentException(string? message) : base(message)
        {
        }
    }
}
