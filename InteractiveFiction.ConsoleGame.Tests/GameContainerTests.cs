namespace InteractiveFiction.ConsoleGame.Tests
{
    public class GameContainerTests
    {
        [Fact]
        public void When_GameCharacterSelected_CreateCharacter()
        {
            GameContainerFixture.GetFixture()

                .HandleCharacterInfoSelected()

                .AssertCharacterCreated();
        }

        [Fact]
        public void When_GameArchetypeSelected_BuildsNewUniverse()
        {
            GameContainerFixture.GetFixture()

                .HandleGameArchetypeSelected()

                .AssertGameArchetypeSelected();
        }

        [Fact]
        public void When_CharacterAndGameIsReady_SendsMoveToGameMessage()
        {
            GameContainerFixture.GetFixture()

                .ReadyCharacterAndGame()

                .AssertMovedToGame();
        }

        [Fact]
        public void When_GetScreen_FromCharacterEvents_ClearsEvents()
        {
            GameContainerFixture.GetFixture()
                .ReadyCharacterAndGame()
                .WithCharacterEvent()

                .GetScreen()

                .AssertScreenHasCharacterEvent();
        }

        [Fact]
        public void When_Perfom_ParsesInputForCharacter()
        {
            GameContainerFixture.GetFixture()
                .ReadyCharacterAndGame()

                .Perform()

                .AssertInputParsedForCharacter();
        }

        [Fact]
        public void When_Tick_TicksUniverse()
        {
            GameContainerFixture.GetFixture()
                .ReadyCharacterAndGame()

                .Tick()

                .AssertUniverseTicked();
        }
    }
}
