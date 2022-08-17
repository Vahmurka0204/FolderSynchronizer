namespace FolderSynchronizerLib
{
    public class LogCreator
    {
        public ILog Create(LogLevels loglevel)
        {
            switch (loglevel)
            {
                case LogLevels.Summary:
                    return new SummaryLog();
                case LogLevels.Silent:
                    return new SilentLog();
                case LogLevels.Verbose:
                    return new VerboseLog();
                default:
                    break;
            }
            return null;
        }
    }
}
