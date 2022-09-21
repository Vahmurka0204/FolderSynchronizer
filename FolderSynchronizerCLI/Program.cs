using FolderSynchronizerLib;

var input = new ConfigurationParser(new LocalPathChecker()).Read(args);
new Launcher().Synchronize(input);