namespace InteractiveFiction.Business.Infrastructure
{
    public class SavedGame
    {
        public string? Name { get; set; }
        public string? URI { get; }

        public SavedGame(FileInfo fileInfo) {
            Name = fileInfo.Name;
            URI = fileInfo.FullName;
        }

        public SavedGame() {
        }
    }
}