using InteractiveFiction.Business.Goal.Statistics;
using InteractiveFiction.Business.Procedure;

namespace InteractiveFiction.Business.Goal
{
    public interface ITracker
    {
        void Track(IProcedure procedure);
        IDictionary<Type, IStat> GetStats();
        void Subscribe<T>(IStatSubscriber subscriber) where T : IStat;
    }
}
