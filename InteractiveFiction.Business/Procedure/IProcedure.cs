namespace InteractiveFiction.Business.Procedure
{
    public interface IProcedure
    {
        IProcedure With<T>(T[] args);
        void Perform();
    }
}
