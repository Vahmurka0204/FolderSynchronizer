using System.IO;
using System.Text.RegularExpressions;

namespace FolderSynchronizerLib
{
    public class TestFolderPathChecker : IChecker
    {
        public bool IsValid(string path)
        {
            Regex r = new Regex(@"^(([a-zA-Z]\:)|(\\))(\\{1}|((\\{1})[^\\]([^/:*?<>""|]*))+)$");
            return r.IsMatch(path);
           
        }
    }
}
