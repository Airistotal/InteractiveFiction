namespace InteractiveFiction.Business.Goal.Questing
{
    public interface IQuest
    {
        void UpdateProgress();
        double GetProgress();
        IReward GetReward();
    }
}
