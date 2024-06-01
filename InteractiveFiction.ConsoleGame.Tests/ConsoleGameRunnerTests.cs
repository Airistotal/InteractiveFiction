namespace InteractiveFiction.ConsoleGame.Tests
{
    public class ConsoleGameRunnerTests
    {
        [Fact]
        public void When_CreateGameRunner_PerformsBoot()
        {
            ConsoleGameRunnerFixture.GetFixture()
                .Init()

                .AssertPerformed("boot");
        }

        [Fact]
        public void When_InMenu_InteractsWithMenu()
        {
            ConsoleGameRunnerFixture.GetFixture()

                .Interact()

                .AssertInteractedWithMenu();
        }

        [Fact]
        public void When_MovedToGame_InteractsWithGame()
        {
            ConsoleGameRunnerFixture.GetFixture()
                .GameContainerReady(true)
                .MoveToGame()

                .Interact()

                .AssertInteractedWithGame();
        }

        [Fact]
        public void When_MoveToGame_GameContainerNotReady_ThrowsException()
        {
            ConsoleGameRunnerFixture.GetFixture()
                .GameContainerReady(false)

                .AssertMoveToGameThrows<CantStartGameException>();
        }
    }
}
