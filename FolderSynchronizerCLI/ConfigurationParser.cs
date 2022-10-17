using FolderSynchronizerLib;

namespace FolderSynchronizerCLI
{
    public class ConfigurationParser
    {
        readonly string _noDelete;
        private readonly IPathChecker _pathChecker;

        public ConfigurationParser(IPathChecker pathChecker)
        {
            _noDelete = "--no-delete";
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

            return configuration;
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

                if (_noDelete != word)
                {
                    throw new SyncException("Invalid word: " + word);
                }

                count++;
            }

            return true;
        }
    }
}
