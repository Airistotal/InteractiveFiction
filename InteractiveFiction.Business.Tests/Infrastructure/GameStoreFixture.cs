using InteractiveFiction.Business.Infrastructure;
using InteractiveFiction.Business.Infrastructure.Game;
using Moq;
using System.IO.Abstractions.TestingHelpers;

namespace InteractiveFiction.Business.Tests.Infrastructure
{
    internal class GameStoreFixture
    {
        private IGameContainer createdGameContainer;
        private GameStore? sut;

        private readonly MockFileSystem fileSystem = new(new Dictionary<string, MockFileData>());
        private readonly Mock<IFactory<GameType, IGameContainer>> gameContainerFactory = new();

        private GameStoreFixture() {
            gameContainerFactory.Setup(x => x.Create(GameType.Generic)).Returns(new Mock<IGameContainer>().Object);
        }

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
            var file = new MockFileData("{\"universe\":\"\", \"characterAgent\":\"\", \"character\":\"\"}");

            fileSystem.AddDirectory(saveDir);
            fileSystem.AddFile(saveDir + "/location.json", file);

            return this;
        }

        public GameStoreFixture Load()
        {
            sut = new GameStore(fileSystem, gameContainerFactory.Object);

            createdGameContainer = sut.Load(sut.Keys.First());

            return this;
        }

        public void AssertLoadedFromAppData()
        {
            Assert.NotNull(createdGameContainer);
        }

        public void AssertLoadThrows<T>() where T : Exception
        {
            Assert.Throws<T>(() => {
                sut = new GameStore(fileSystem, gameContainerFactory.Object);

                createdGameContainer = sut.Load(new SavedGame());
            });
        }
    }
}
