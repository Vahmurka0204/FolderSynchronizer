namespace FolderSynchronizerLib
{
    public class FolderSet
    {
        public readonly List<FolderPair> FolderList;
        public readonly bool NoDeleteFlag;
        public readonly LogLevels Loglevel;

        public FolderSet(Configuration configuration)
        {
            var folderSnapshotManager = new FolderSnapshotManager();
            FolderList = new List<FolderPair>();
            foreach (var path in configuration.FoldersPaths)
            {
                var folderPair = new FolderPair(folderSnapshotManager.DeserializeFolderSnapshot(path), folderSnapshotManager.MakeFolderSnapshot(path));
                FolderList.Add(folderPair);
            }
                        
            NoDeleteFlag = configuration.NoDeleteFlag;            
            Loglevel = configuration.LogLevel;
        }

        public FolderSet() { }
    }
}
