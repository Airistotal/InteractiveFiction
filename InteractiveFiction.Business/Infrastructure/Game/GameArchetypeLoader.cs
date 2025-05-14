using System.IO.Abstractions;

namespace InteractiveFiction.Business.Infrastructure.Game
{
    public class GameArchetypeLoader : IGameArchetypeLoader
    {
        private readonly IFileSystem fileSystem;

        public GameArchetypeLoader(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        public List<GameArchetype> LoadGameArchetypes()
        {
            var gameArchetypes = new List<GameArchetype>();

            foreach (var dirPath in fileSystem.Directory.GetDirectories("games")) {
                var dir = new DirectoryInfo(dirPath);
                gameArchetypes.Add(new GameArchetype() { Name = dir.Name });
            }

            return gameArchetypes;
        }
    }
}
