namespace InteractiveFiction.ConsoleGame.Sanitize.Argument
{
    public class NoSuchDirectionException : Exception
    {
        public NoSuchDirectionException(string arg) : base($"No such direction: {arg}")
        {
        }
    }
}