using FolderSynchronizerLib;

var input = new InputDataReader(new FolderPathChecker()).Read(args);
new Launcher().Synchronize(input);