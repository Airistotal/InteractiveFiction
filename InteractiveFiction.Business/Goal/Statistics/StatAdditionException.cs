namespace InteractiveFiction.Business.Goal.Statistics
{
    public class StatAdditionException : Exception
    {
        public StatAdditionException(Type expected, Type actual) :
            base("Unable to add " + actual.Name + " to " + expected.Name)
        {
            
        }
    }
}
