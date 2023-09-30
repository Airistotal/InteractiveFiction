using InteractiveFiction.Business.Goal;
using InteractiveFiction.Business.Goal.Statistics;
using InteractiveFiction.Business.Procedure;
using Moq;

namespace InteractiveFiction.Business.Tests.Goal.Statistics
{
    public class StatTrackerTests
    {
        [Fact]
        public void When_TrackProcedure_AddsToStats()
        {
            var stat = new Mock<IStat>();
            var procedure = new Mock<IProcedure>();
            procedure.Setup(_ => _.GetAsStat()).Returns(stat.Object);
            var stat2 = new Mock<IStat>();
            var procedure2 = new Mock<IProcedure>();
            procedure2.Setup(_ => _.GetAsStat()).Returns(stat2.Object);
            var sut = new StatTracker();

            sut.Track(procedure.Object);
            sut.Track(procedure2.Object);
            var stats = sut.GetStats();

            Assert.NotNull(stats);
            Assert.NotEmpty(stats);
            procedure.Verify(_ => _.GetAsStat(), Times.Once);
            stat.Verify(_ => _.Add(It.IsAny<IStat>()));
            
        }

        [Fact]
        public void When_SubscribeToStat_SendsNotificationOnTrack()
        {
            var procedure = new Mock<IProcedure>();
            procedure.Setup(_ => _.GetAsStat()).Returns(new TestStat());
            var procedure2 = new Mock<IProcedure>();
            procedure2.Setup(_ => _.GetAsStat()).Returns(new TestStat2());
            var statSubscriber = new Mock<IStatSubscriber>();
            var statSubscriber2 = new Mock<IStatSubscriber>();
            var sut = new StatTracker();

            sut.Subscribe<TestStat>(statSubscriber.Object);
            sut.Subscribe<TestStat2>(statSubscriber2.Object);
            sut.Track(procedure.Object);
            sut.Track(procedure2.Object);

            statSubscriber.Verify(_ => _.callback(It.IsAny<TestStat>()), Times.Once);
            statSubscriber2.Verify(_ => _.callback(It.IsAny<TestStat2>()), Times.Once);
        }

        private class TestStat : IStat
        {
            public void Add(IStat stat)
            {
                throw new NotImplementedException();
            }
        }
        private class TestStat2 : IStat
        {
            public void Add(IStat stat)
            {
                throw new NotImplementedException();
            }
        }
    }
}
