namespace InteractiveFiction.Business.Tests.Goal.Statistics
{
    public class StatTrackerTests
    {
        [Fact]
        public void When_AddStat_AddsToStats()
        {
            StatTrackerFixture.GetFixture()
                .WithExistingStat()
                
                .AddStat()

                .AssertStatAdded();
        }

        [Fact]
        public void When_SubscribeToStat_SendsNotificationOnTrack()
        {
            StatTrackerFixture.GetFixture()
                .WithObserver()

                .AddStat()

                .AssertObserverIsSubscribed();
        }
    }
}
