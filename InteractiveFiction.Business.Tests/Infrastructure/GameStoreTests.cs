using Xunit.Sdk;
using InteractiveFiction.Business.Infrastructure;

namespace InteractiveFiction.Business.Tests.Infrastructure
{
    public class GameStoreTests
    {
        [Fact]
        public void When_LoadGame_NoDirectory_NoFile_Throws()
        {
            GameStoreFixture.GetFixture()

                .AssertLoadThrows<NoGameException>();
        }
        [Fact]
        public void When_LoadGame_NoFile_Throws()
        {
            GameStoreFixture.GetFixture()
                .WithSaveDir()

                .AssertLoadThrows<NoGameException>();
        }

        [Fact]
        public void When_LoadGame_GetsFile()
        {
            GameStoreFixture.GetFixture()
                .WithFile()

                .Load()

                .AssertLoadedFromAppData();
        }
    }
}
