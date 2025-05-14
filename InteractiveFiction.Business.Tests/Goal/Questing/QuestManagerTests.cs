namespace InteractiveFiction.Business.Tests.Goal.Questing
{
    public class QuestManagerTests
    {
        [Fact]
        public void When_AddQuest_AddsQuest()
        {
            QuestManagerFixture.GetFixture()

                .AddQuest()

                .AssertQuestAdded();
        }

        [Fact]
        public void When_QuestIsComplete_ReturnsRewards()
        {
            QuestManagerFixture.GetFixture()
                .WithCompleteQuest()
                .AddQuest()

                .GetRewards()

                .AssertGotRewards();
        }

        [Fact]
        public void When_ObserverMethodsCalled_Passthrough()
        {
            QuestManagerFixture.GetFixture()

                .CallObserverMethods()

                .AssertFunctionsSentToObserver();
        }
    }
}
