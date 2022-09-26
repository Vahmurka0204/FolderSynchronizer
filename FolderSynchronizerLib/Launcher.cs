namespace FolderSynchronizerLib
{
    public class Launcher
    {
        private readonly IFolderSnapshotManager _snapshotManager;

        public Launcher(IFolderSnapshotManager manager)
        {
            _snapshotManager = manager;
        }

        public void Synchronize(Configuration input)
        {
            var folderSet = new FolderSet(input);
            
            var syncInstructions = new SyncDataReader().Load(folderSet);
            var logger = new LoggerFactory().Create(folderSet.Loglevel);
            new SyncProcessor().Synchronize(syncInstructions, input.FoldersPaths, logger);
            Console.WriteLine(logger.FormLogToPrint());

            foreach(var path in input.FoldersPaths)
            {
                _snapshotManager.SerializeFolderSnapshot(path);
            }
        }
    }
}
