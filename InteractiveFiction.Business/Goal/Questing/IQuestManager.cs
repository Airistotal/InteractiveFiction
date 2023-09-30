namespace InteractiveFiction.Business.Goal.Questing
{
    public interface IQuestManager
    {
        void AddQuest(IQuest quest);
        IList<IReward> GetRewards();
    }
}