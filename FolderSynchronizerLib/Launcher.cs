namespace FolderSynchronizerLib
{
    public class Launcher
    {
        private readonly IFolderSnapshotManager _snapshotManager;

        public Launcher(IFolderSnapshotManager manager)
        {
            _snapshotManager = manager;
        }

        public void Synchronize(InputData input)
        {
            var folderSet = new FolderSet(input);
            
            var syncData = new SyncDataReader().Load(folderSet);
            var log = new LogCreator().Create(folderSet.Loglevel);
            new SyncProcessor().Synchronize(syncData, input.FoldersPaths, log);
            Console.WriteLine(log.FormLogToPrint());

            foreach(var path in input.FoldersPaths)
            {
                _snapshotManager.SerializeFolderSnapshot(path);
            }
        }
    }
}
