using InteractiveFiction.Business.Goal.Questing;
using InteractiveFiction.Business.Goal;
using Moq;

namespace InteractiveFiction.Business.Tests.Goal.Questing
{
    internal class QuestManagerFixture
    {
        private QuestManager sut;
        private IList<IReward> rewards;
        private readonly Mock<IObserver<IStat>> questAsStatObserver = new();
        private readonly Mock<IObserver<IStat>> statObserver = new();
        private Mock<IQuest> quest;
        private Mock<IObservable<IStat>> statObservable = new();

        private QuestManagerFixture() {
            sut = new QuestManager(statObserver.Object, statObservable.Object);
        }

        public static QuestManagerFixture GetFixture() { return new QuestManagerFixture(); } 

        public QuestManagerFixture WithCompleteQuest()
        {
            quest ??= questAsStatObserver.As<IQuest>();
            quest.Setup(_ => _.GetProgress()).Returns(1);
            quest.Setup(_ => _.GetReward()).Returns(new Mock<IReward>().Object);

            return this;
        }

        public QuestManagerFixture AddQuest()
        {
            quest ??= questAsStatObserver.As<IQuest>();
            sut.AddQuest(quest.Object);

            return this; 
        }

        public QuestManagerFixture GetRewards()
        {
            rewards = sut.GetRewards();

            return this;
        }

        public QuestManagerFixture CallObserverMethods()
        {
            sut.OnNext(new Mock<IStat>().Object);
            sut.OnCompleted();
            sut.OnError(new Exception());

            return this;
        }

        public void AssertQuestAdded()
        {
            Assert.NotNull(sut.GetQuests());
            Assert.NotEmpty(sut.GetQuests());
            statObservable.Verify(_ => _.Subscribe(questAsStatObserver.Object), Times.Once);
        }

        public void AssertGotRewards()
        {
            quest.Verify(_ => _.GetProgress(), Times.Once);
            quest.Verify(_ => _.GetReward(), Times.Once);
            Assert.NotNull(rewards);
            Assert.NotEmpty(rewards);
        }

        public void AssertFunctionsSentToObserver()
        {
            statObserver.Verify(_ => _.OnNext(It.IsAny<IStat>()), Times.Once);
            statObserver.Verify(_ => _.OnCompleted(), Times.Once);
            statObserver.Verify(_ => _.OnError(It.IsAny<Exception>()), Times.Once);
        }
    }
}
