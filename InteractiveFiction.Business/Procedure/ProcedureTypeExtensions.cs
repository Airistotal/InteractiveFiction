namespace InteractiveFiction.Business.Procedure
{
    public static class ProcedureTypeExtensions
    {
        public static string Name(this ProcedureType type)
        {
            return type switch
            {
                ProcedureType.Move => "Move",
                ProcedureType.NULL => "do nothing",
                _ => string.Empty,
            };
        }
    }
}
