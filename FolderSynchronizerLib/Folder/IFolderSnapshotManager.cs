namespace FolderSynchronizerLib
{
    public interface IFolderSnapshotManager
    {
        FolderSnapshot DeserializeFolderSnapshot(string path);
        FolderSnapshot MakeFolderSnapshot(string path);
        void SerializeFolderSnapshot(string path);
    }
}