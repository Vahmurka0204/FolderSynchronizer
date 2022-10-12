using FolderSynchronizerLib;

Logger.AddLogger(new ConsoleLogger());
string loggerFileName = DateTime.Now.ToString("dd MM yyyy") + ".txt";
Logger.AddLogger(new FileLogger(loggerFileName));
var configuration = new ConfigurationParser(new LocalPathChecker()).Read(args);
new Launcher(new FolderSnapshotManager()).Synchronize(configuration);