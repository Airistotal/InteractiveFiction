namespace InteractiveFiction.Business.Goal.Statistics
{
    public interface IStatSubscriber
    {
        void callback(IStat stat);
    }
}
