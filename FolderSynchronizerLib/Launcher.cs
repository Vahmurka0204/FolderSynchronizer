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
            var syncStrategy = new StrategyFactory().Create(folderSet.NoDeleteFlag);
            var syncInstructions = syncStrategy.MakeSyncInstruction(folderSet);
            new SyncProcessor().Synchronize(syncInstructions, input.FoldersPaths);

            foreach(var path in input.FoldersPaths)
            {
                _snapshotManager.SerializeFolderSnapshot(path);
            }
        }
    }
}
