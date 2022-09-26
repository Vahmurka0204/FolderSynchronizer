using FolderSynchronizerLib;

var configuration = new ConfigurationParser(new LocalPathChecker()).Read(args);
new Launcher(new FolderSnapshotManager()).Synchronize(configuration);