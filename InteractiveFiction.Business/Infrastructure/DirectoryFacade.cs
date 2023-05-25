namespace InteractiveFiction.Business.Infrastructure
{
    public class DirectoryFacade : IDirectoryFacade
    {
        public string[] GetDirectories(string path)
        {
            return Directory.GetDirectories(path);
        }
    }
}
