namespace FolderSynchronizerLib
{
    public class ConfigurationParser
    {
        readonly string _noDelete;
        readonly string _loglevel;
        private readonly IPathChecker _pathChecker;

        public ConfigurationParser(IPathChecker pathChecker)
        {
            _noDelete = "--no-delete";
            _loglevel = "--loglevel";
            _pathChecker = pathChecker;
        }

        public Configuration Read(string[] args)
        {
            var configuration = new Configuration();

            if (!IsConfigurationValid(args))
            {
                throw new SyncException("Configuration is invalid");
            }

            int count = 0;

            while (count < 2 && _pathChecker.IsValid(args[count]))
            {
                configuration.FoldersPaths.Add(args[count]);
                count++;
            }

            var flagList = new List<string>();

            while (count < args.Length)
            {
                flagList.Add(args[count]);
                count++;
            }

            if (flagList.Contains(_noDelete))
            {
                configuration.NoDeleteFlag = true;
            }

            if (flagList.Contains(_loglevel))
            {
                configuration.LogLevel = GetLogFlag(flagList);
            }

            return configuration;
        }

        private LogLevels GetLogFlag(List<string> flagList)
        {
            string logFlag;

            try
            {
                logFlag = flagList[flagList.IndexOf(_loglevel) + 1];
            }
            catch (Exception)
            {
                throw new SyncException("Do not specify the type of logging");
            }

            LogLevels? validFlag = GetUserLogLevel(logFlag);

            if (validFlag == null)
            {
                throw new SyncException("Invalid type of logging");
            }

            return (LogLevels)validFlag;
        }

        private LogLevels? GetUserLogLevel(string logLevel)
        {
            var validLevels = Enum.GetNames(typeof(LogLevels));

            foreach (var level in validLevels)
            {
                if (level.ToLower() == logLevel.ToLower())
                {
                    return (LogLevels)Enum.Parse(typeof(LogLevels), level);
                }
            }

            return null;
        }

        public bool IsConfigurationValid(string[] args)
        {
            if (args.Length < 2)
            {
                throw new SyncException("Invalid format command");
            }

            int count = 0;

            while (count < 2 && _pathChecker.IsValid(args[count]))
            {
                count++;
            }

            if (count != 2)
            {
                throw new SyncException("Invalid path");
            }

            while (count < args.Length)
            {
                string word = args[count];

                if (GetUserLogLevel(word) == null && _noDelete != word && _loglevel != word)
                {
                    throw new SyncException("Invalid word: " + word);
                }

                count++;
            }

            return true;
        }
    }
}
