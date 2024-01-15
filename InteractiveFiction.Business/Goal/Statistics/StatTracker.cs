using InteractiveFiction.Business.Infrastructure;

namespace InteractiveFiction.Business.Goal
{
    public class StatTracker : IObserver<IStat>, IObservable<IStat>
    {
        private readonly IDictionary<Type, IStat> stats = new Dictionary<Type, IStat>();
        private readonly IList<IObserver<IStat>> observers = new List<IObserver<IStat>>();

        public IDictionary<Type, IStat> GetStats()
        {
            return stats;
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(IStat stat)
        {
            if (!stats.ContainsKey(stat.GetType()))
            {
                stats.Add(stat.GetType(), stat);
            }
            else
            {
                stats[stat.GetType()].Add(stat);
            }

            foreach (IObserver<IStat> observer in observers)
            {
                observer.OnNext(stats[stat.GetType()]);
            }
        }

        public IDisposable Subscribe(IObserver<IStat> observer)
        {
            observers.Add(observer);

            return new DefaultObserverRemover(observers, observer);
        }
    }
}
