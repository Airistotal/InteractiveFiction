namespace InteractiveFiction.Business.Goal
{
    public interface IObserverFactory
    {
        IObserver<IStat> Create(ObserverType type);
    }
}