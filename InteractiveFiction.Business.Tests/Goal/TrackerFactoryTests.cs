using InteractiveFiction.Business.Goal;
using InteractiveFiction.Business.Goal.Questing;

namespace InteractiveFiction.Business.Tests.Goal
{
    public class TrackerFactoryTests
    {
        [Theory]
        [InlineData(ObserverType.Stat, typeof(StatTracker))]
        [InlineData(ObserverType.Quest, typeof(QuestManager))]
        public void When_CreateStatTracker_ReturnsStatTracker(ObserverType type, Type expected)
        {
            ObserverFactory sut = new ObserverFactory();

            IObserver<IStat> tracker = sut.Create(type);

            Assert.NotNull(tracker);
            Assert.IsType(expected, tracker);
        }
    }
}
