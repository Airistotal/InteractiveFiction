namespace InteractiveFiction.Business.Procedure
{
    public class NullProcedure : IProcedure
    {
        public void Perform() { }

        public IProcedure With<T>(T[] args) { return this; }
    }
}
