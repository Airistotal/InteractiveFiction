namespace InteractiveFiction.Business.Entity
{
    public interface IEntityBuilder
    {
        IEntityBuilder From(string path);
        IEntityBuilder Character(string? name);
        IEntity Build();
    }
}
