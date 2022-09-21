namespace FolderSynchronizerLib
{
    public interface ISyncDataReaderStrategy
    {
        SyncInstructions MakeSyncData(FolderSet folderSet);
    }
}
