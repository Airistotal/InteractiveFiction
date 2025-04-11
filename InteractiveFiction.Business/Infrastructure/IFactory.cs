namespace InteractiveFiction.Business.Infrastructure
{
    public interface IFactory<Type, Instance>
    {
        Instance Create(Type type);
    }
}
