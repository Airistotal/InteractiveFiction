using System.IO.Abstractions;
using System.Text.Json;

namespace InteractiveFiction.Business.Infrastructure.Game
{
    public class GameStore : IStore<SavedGame, IGameContainer>
    {

        private readonly string appData;
        private readonly string saveDir;
        private readonly IFileSystem FileSystem;
        private readonly IFactory<GameType, IGameContainer> GameContainerFactory;

        public GameStore(IFileSystem fileSystem, IFactory<GameType, IGameContainer> gameContainerFactory) {
            FileSystem = fileSystem;
            GameContainerFactory = gameContainerFactory;

            appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            saveDir = appData + "/IF/saves";
        }

        public List<SavedGame> Keys {
            get {
                var saves = new List<SavedGame>();

                if (FileSystem.Directory.Exists(saveDir))
                {
                    foreach (var fileName in FileSystem.Directory.GetFiles(saveDir))
                    {
                        var file = new FileInfo(fileName);
                        saves.Add(new SavedGame(file));
                    }
                }

                return saves;
            }
        }

        public IGameContainer Load(SavedGame savedGame)
        {
            if (FileSystem.Directory.Exists(saveDir))
            {
                using var fs = FileSystem.FileStream.New(saveDir + "/" + savedGame.Name, FileMode.Open);
                if (fs == null || fs.Length == 0)
                {
                    throw new NoGameException();
                }

                var saveData = JsonSerializer.Deserialize<SaveData>(fs);
                var gameContainer = GameContainerFactory.Create(GameType.Generic);
                gameContainer.Load(saveData);
                
                return gameContainer;
            }

            throw new NoGameException();
        }

        public void Save(SavedGame savedGame, IGameContainer value)
        {
            if (FileSystem.Directory.Exists(saveDir))
            {
                FileSystem.Directory.CreateDirectory(saveDir);
            }

            using var fs = FileSystem.FileStream.New(saveDir + "/" + savedGame.Name, FileMode.OpenOrCreate);
            fs.SetLength(0);

            JsonSerializer.Serialize(fs, value.GetSaveData());
        }
    }
}
