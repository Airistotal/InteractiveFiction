using InteractiveFiction.Business.Goal;
using InteractiveFiction.Business.Goal.Questing;
using InteractiveFiction.Business.Procedure;
using Moq;

namespace InteractiveFiction.Business.Tests.Goal.Questing
{
    public class QuestManagerTests
    {
        [Fact]
        public void When_AddQuest_ConnectsToStatTracker()
        {
            var stat = new Mock<IStat>();
            var quest = new Mock<IQuest>();
            var statTracker = new Mock<ITracker>();
            var sut = new QuestManager(statTracker.Object);
            
            sut.AddQuest(quest.Object);
            var quests = sut.GetQuests();

            Assert.NotNull(quests);
            Assert.NotEmpty(quests);
            quest.Verify(_ => _.UseTracker(statTracker.Object), Times.Once);
        }

        [Fact]
        public void When_QuestIsComplete_ReturnsRewards()
        {
            var quest = GetCompleteQuest();
            var statTracker = new Mock<ITracker>();
            var sut = new QuestManager(statTracker.Object);
            sut.AddQuest(quest.Object);

            IList<IReward> rewards = sut.GetRewards();

            quest.Verify(_ => _.GetProgress(), Times.Once);
            quest.Verify(_ => _.GetReward(), Times.Once);
            Assert.NotNull(rewards);
            Assert.NotEmpty(rewards);
        }

        [Fact]
        public void When_TrackerMethodsCalled_Passthrough()
        {
            var statTracker = new Mock<ITracker>();
            var sut = new QuestManager(statTracker.Object);

            sut.GetStats();
            sut.Track(new Mock<IProcedure>().Object);

            statTracker.Verify(_ => _.GetStats(), Times.Once);
            statTracker.Verify(_ => _.Track(It.IsAny<IProcedure>()), Times.Once);
        }

        private Mock<IQuest> GetCompleteQuest()
        {
            var quest = new Mock<IQuest>();
            quest.Setup(_ => _.GetProgress()).Returns(1);
            quest.Setup(_ => _.GetReward()).Returns(new Mock<IReward>().Object);

            return quest;
        }
    }
}
