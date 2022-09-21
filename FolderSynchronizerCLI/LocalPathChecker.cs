using System.IO;

namespace FolderSynchronizerLib
{
    public class LocalPathChecker : IPathChecker
    {
        public bool IsValid(string path)
        {
            return Directory.Exists(path);
        }
    }
}
