// TODO : RemoveCollision

namespace FolderSynchronizerLib
{
    public class SyncDataReader
    {
        public ISyncDataReaderStrategy SyncReaderStrategy { get; set; }

        
        public SyncData Load(FolderSet folderSet)
        {
            if (folderSet.NoDeleteFlag)
            {
                SyncReaderStrategy = new SyncDataReaderNoDeleteStrategy();
            }
            else
            {
                SyncReaderStrategy = new SyncDataReaderDeleteStrategy();
            }

            var syncData = SyncReaderStrategy.MakeSyncData(folderSet);

            return syncData;
        }

        
    }
}
