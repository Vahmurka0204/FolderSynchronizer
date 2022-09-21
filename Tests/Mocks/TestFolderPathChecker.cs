using System.Text.RegularExpressions;
using FolderSynchronizerLib;

namespace Tests
{
    public class TestFolderPathChecker : IPathChecker
    {
        public bool IsValid(string path)
        {
            Regex r = new Regex(@"^(([a-zA-Z]\:)|(\\))(\\{1}|((\\{1})[^\\]([^/:*?<>""|]*))+)$");
            return r.IsMatch(path);
           
        }
    }
}
