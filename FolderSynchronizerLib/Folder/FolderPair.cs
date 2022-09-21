namespace FolderSynchronizerLib
{
    public class FolderPair
    {
        public readonly FolderSnapshot Old;
        public readonly FolderSnapshot New;

        public FolderPair(FolderSnapshot oldFolder, FolderSnapshot newFolder)
        {
            Old = oldFolder;
            New = newFolder;
        }
    }
}
