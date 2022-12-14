using FolderSynchronizerLib;

namespace FolderSynchronizerCLI
{
    public class LocalPathChecker : IPathChecker
    {
        public bool IsValid(string path)
        {
            return Directory.Exists(path);
        }
    }
}
