namespace InteractiveFiction.Business.Goal.Questing
{
    public interface IQuest
    {
        void UpdateProgress();
        void UseTracker(ITracker tracker);
        double GetProgress();
        IReward GetReward();
    }
}
