namespace InteractiveFiction.Business.Infrastructure
{
    public class GameArchetypeLoader : IGameArchetypeLoader
    {
        private readonly IDirectoryFacade directoryFacade;

        public GameArchetypeLoader(IDirectoryFacade directoryFacade)
        {
            this.directoryFacade = directoryFacade;
        }

        public List<GameArchetype> LoadGameArchetypes()
        {
            var gameArchetypes = new List<GameArchetype>();

            foreach (var dirPath in this.directoryFacade.GetDirectories("games")) {
                var dir = new DirectoryInfo(dirPath);
                gameArchetypes.Add(new GameArchetype() { Name = dir.Name });
            }

            return gameArchetypes;
        }
    }
}
