namespace FolderSynchronizerLib
{
    public class Configuration
    {
        public List<string> FoldersPaths;
        public bool NoDeleteFlag;
        public LogLevels LogLevel;

        public Configuration()
        {
            FoldersPaths = new List<string>();
            NoDeleteFlag = false;
            LogLevel = LogLevels.Summary;
        }
    }
}
