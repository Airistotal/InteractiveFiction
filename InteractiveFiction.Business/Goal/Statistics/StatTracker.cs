using InteractiveFiction.Business.Goal.Statistics;
using InteractiveFiction.Business.Procedure;

namespace InteractiveFiction.Business.Goal
{
    public class StatTracker : ITracker
    {
        IDictionary<Type, IStat> stats = new Dictionary<Type, IStat>();
        IDictionary<Type, IList<IStatSubscriber>> subscribers = new Dictionary<Type, IList<IStatSubscriber>>();

        public IDictionary<Type, IStat> GetStats()
        {
            return stats;
        }

        public void Subscribe<T>(IStatSubscriber subscriber) where T : IStat
        {
            if (!subscribers.ContainsKey(typeof(T)))
            {
                subscribers.Add(typeof(T), new List<IStatSubscriber>());
            }

            subscribers[typeof(T)].Add(subscriber);
        }

        public void Track(IProcedure procedure)
        {
            IStat stat = procedure.GetAsStat();
            if (!stats.ContainsKey(stat.GetType()))
            {
                stats.Add(stat.GetType(), stat);
            } else
            {
                stats[stat.GetType()].Add(stat);
            }
            
            if (subscribers.ContainsKey(stat.GetType()))
            {
                foreach (IStatSubscriber statSubscriber in subscribers[stat.GetType()])
                {
                    statSubscriber.callback(stats[stat.GetType()]);
                }
            }
        }
    }
}
