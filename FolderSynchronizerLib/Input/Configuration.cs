namespace FolderSynchronizerLib
{
    public class Configuration
    {
        public List<string> FoldersPaths;
        public bool NoDeleteFlag;

        public Configuration()
        {
            FoldersPaths = new List<string>();
            NoDeleteFlag = false;
        }
    }
}
