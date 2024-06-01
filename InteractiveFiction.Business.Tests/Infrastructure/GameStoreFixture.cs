using InteractiveFiction.Business.Infrastructure;
using InteractiveFiction.ConsoleGame;
using System.IO.Abstractions.TestingHelpers;

namespace InteractiveFiction.Business.Tests.Infrastructure
{
    internal class GameStoreFixture
    {
        private GameContainer? gameContainer;

        private GameStore? sut;

        private readonly MockFileSystem fileSystem = new(new Dictionary<string, MockFileData>());

        private GameStoreFixture() { }

        public static GameStoreFixture GetFixture() { return new GameStoreFixture(); }

        public GameStoreFixture WithSaveDir()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var saveDir = appData + "/IF/saves";

            fileSystem.AddDirectory(saveDir);

            return this;
        }

        public GameStoreFixture WithFile()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var saveDir = appData + "/IF/saves";
            var file = new MockFileData("{}");

            fileSystem.AddDirectory(saveDir);
            fileSystem.AddFile(saveDir + "/location.json", file);

            return this;
        }

        public GameStoreFixture Load()
        {
            sut = new GameStore(fileSystem);

            gameContainer = sut.Load(sut.Keys.First());

            return this;
        }

        public void AssertLoadedFromAppData()
        {
            Assert.NotNull(gameContainer);
        }

        public void AssertLoadThrows<T>() where T : Exception
        {
            Assert.Throws<T>(() => {
                sut = new GameStore(fileSystem);

                gameContainer = sut.Load(new SavedGame());
            });
        }
    }
}
