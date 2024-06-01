using InteractiveFiction.ConsoleGame;
using System.IO.Abstractions;
using System.Text.Json;

namespace InteractiveFiction.Business.Infrastructure
{
    public class GameStore : IStore<SavedGame, GameContainer>
    {

        private readonly string appData;
        private readonly string saveDir;
        private readonly IFileSystem FileSystem;

        public GameStore(IFileSystem fileSystem) {
            FileSystem = fileSystem;

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

        public GameContainer Load(SavedGame savedGame)
        {
            if (FileSystem.Directory.Exists(saveDir))
            {
                using var fs = FileSystem.FileStream.New(saveDir + "/" + savedGame.Name, FileMode.Open);
                if (fs == null || fs.Length == 0)
                {
                    throw new NoGameException();
                }

                return JsonSerializer.Deserialize<GameContainer>(fs);
            }

            throw new NoGameException();
        }

        public void Save(SavedGame savedGame, GameContainer value)
        {
            if (FileSystem.Directory.Exists(saveDir))
            {
                FileSystem.Directory.CreateDirectory(saveDir);
            }

            using var fs = FileSystem.FileStream.New(saveDir + "/" + savedGame.Name, FileMode.OpenOrCreate);
            fs.SetLength(0);

            JsonSerializer.Serialize(fs, value);
        }
    }
}
