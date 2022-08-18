namespace FolderSynchronizerLib
{
    public class InputDataReader
    {
        readonly string _noDelete;
        readonly string _loglevel;
        private readonly IChecker _pathChecker;

        public InputDataReader(IChecker checker)
        {
            _noDelete = "--no-delete";
            _loglevel = "--loglevel";
            _pathChecker = checker;
        }

        public InputData Read(string[] args)
        {
            var input = new InputData();

            if (!IsInputValid(args))
            {
                throw new SyncException("Input is invalid");
            }

            int count = 0;

            while (count < 2 && _pathChecker.IsValid(args[count]))
            {
                input.FoldersPaths.Add(args[count]);
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
                input.NoDeleteFlag = true;
            }

            if (flagList.Contains(_loglevel))
            {
                input.LogLevel = GetLogFlag(flagList);
            }

            return input;
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

        public bool IsInputValid(string[] args)
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
