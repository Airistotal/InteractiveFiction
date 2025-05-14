using InteractiveFiction.Business.Goal;

namespace InteractiveFiction.Business.Infrastructure
{
    public class DefaultObserverRemover : IDisposable
    {
        private IList<IObserver<IStat>> observers;
        private IObserver<IStat> observer;

        public DefaultObserverRemover(IList<IObserver<IStat>> observers, IObserver<IStat> observer) {
            this.observers = observers;
            this.observer = observer;
        }

        public void Dispose() {
            observers.Remove(observer);
        }
    }
}
