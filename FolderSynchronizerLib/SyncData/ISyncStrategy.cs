namespace FolderSynchronizerLib
{
    public interface ISyncStrategy
    {
        SyncInstructions MakeSyncInstruction(FolderSet folderSet);
    }
}
