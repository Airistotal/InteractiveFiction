namespace InteractiveFiction.Business.Goal.Questing
{
    public class QuestManager : IQuestManager, IObserver<IStat>
    {
        private readonly IObserver<IStat> observer;

        private readonly IObservable<IStat> observable;

        private readonly IList<IQuest> quests;

        public QuestManager(IObserver<IStat> observer, IObservable<IStat> observable)
        {
            this.quests = new List<IQuest>();
            this.observer = observer;
            this.observable = observable;
        }

        public void AddQuest(IQuest quest)
        {
            if (quest is IObserver<IStat> observerQuest)
            {
                observable.Subscribe(observerQuest);
            }

            quests.Add(quest);
        }

        public IList<IQuest> GetQuests()
        {
            return quests;
        }

        public IList<IReward> GetRewards()
        {
            IList<IReward> rewards = new List<IReward>();

            foreach (var quest in quests)
            {
                if (quest.GetProgress() == 1)
                {
                    rewards.Add(quest.GetReward());
                }
            }

            return rewards;
        }

        public void OnCompleted()
        {
            observer.OnCompleted();
        }

        public void OnError(Exception error)
        {
            observer.OnError(error);
        }

        public void OnNext(IStat stat)
        {
            observer.OnNext(stat);
        }
    }
}
