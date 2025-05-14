using InteractiveFiction.Business.Goal.Questing;

namespace InteractiveFiction.Business.Goal
{
    public class ObserverFactory : IObserverFactory
    {
        public ObserverFactory() { }

        public IObserver<IStat> Create(ObserverType type)
        {
            switch (type)
            {
                case ObserverType.Stat: return new StatTracker();
                case ObserverType.Quest:
                    var statTracker = new StatTracker();
                    return new QuestManager(statTracker, statTracker);
                default: throw new NotImplementedException();
            }
        }
    }
}
