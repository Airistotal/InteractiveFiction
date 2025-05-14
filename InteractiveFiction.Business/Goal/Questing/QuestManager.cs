using InteractiveFiction.Business.Goal.Statistics;
using InteractiveFiction.Business.Procedure;

namespace InteractiveFiction.Business.Goal.Questing
{
    public class QuestManager : IQuestManager, ITracker
    {
        private readonly ITracker tracker;

        private readonly IList<IQuest> quests;

        public QuestManager(ITracker tracker)
        {
            this.quests = new List<IQuest>();
            this.tracker = tracker;
        }

        public void AddQuest(IQuest quest)
        {
            quest.UseTracker(tracker);
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

        public IDictionary<Type, IStat> GetStats()
        {
            return tracker.GetStats();
        }

        public void Subscribe<T>(IStatSubscriber subscriber) where T : IStat
        {
            tracker.Subscribe<T>(subscriber);
        }

        public void Track(IProcedure procedure)
        {
            tracker.Track(procedure);
        }
    }
}
