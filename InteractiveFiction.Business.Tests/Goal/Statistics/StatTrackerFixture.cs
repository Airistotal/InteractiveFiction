using InteractiveFiction.Business.Goal;
using InteractiveFiction.Business.Goal.Statistics;
using Moq;

namespace InteractiveFiction.Business.Tests.Goal.Statistics
{
    internal class StatTrackerFixture
    {
        private StatTracker sut;
        private Mock<IStat> existingStat;
        private Mock<IObserver<IStat>> observer = new();

        private StatTrackerFixture() {
            sut = new StatTracker();
        }

        public static StatTrackerFixture GetFixture()
        {
            return new StatTrackerFixture();
        }

        public StatTrackerFixture WithExistingStat()
        {
            existingStat = new Mock<IStat>();
            sut.OnNext(existingStat.Object);

            return this;
        }

        public StatTrackerFixture WithObserver()
        {
            sut.Subscribe(observer.Object);

            return this;
        }

        public StatTrackerFixture AddStat()
        {
            sut.OnNext(new Mock<IStat>().Object);

            return this;
        }

        public void AssertStatAdded()
        {
            Assert.NotNull(sut.GetStats());
            Assert.NotEmpty(sut.GetStats());
            existingStat.Verify(_ => _.Add(It.IsAny<IStat>()));
        }

        public void AssertObserverIsSubscribed()
        {
            observer.Verify(_ => _.OnNext(It.IsAny<IStat>()), Times.Once);
        }
    }
}
