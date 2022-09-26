// TODO : RemoveCollision

namespace FolderSynchronizerLib
{
    public class SyncDataReader
    {
        public ISyncStrategy SyncStrategy { get; set; }

        
        public SyncInstructions Load(FolderSet folderSet)
        {
            if (folderSet.NoDeleteFlag)
            {
                SyncStrategy = new SyncNoDeleteStrategy();
            }
            else
            {
                SyncStrategy = new SyncDeleteStrategy();
            }

            var syncInstructions = SyncStrategy.MakeSyncInstruction(folderSet);

            return syncInstructions;
        }

        
    }
}
