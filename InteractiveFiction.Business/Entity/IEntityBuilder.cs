namespace InteractiveFiction.Business.Entity
{
    public interface IEntityBuilder
    {
        IEntityBuilder FromLines(IEnumerable<string> lines);
        IEntity Build();
    }
}
