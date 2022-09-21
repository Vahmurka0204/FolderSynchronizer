namespace FolderSynchronizerLib
{
    public class SyncInstructions : IEquatable<SyncInstructions>
    {
        public readonly List<FileDescriptor> FilesToCopy;
        public readonly List<FileDescriptor> FilesToUpdate;
        public readonly List<FileDescriptor> FilesToDelete;

        public SyncInstructions
           (List<FileDescriptor> filesToCopy,
            List<FileDescriptor> filesToUpdate,
            List<FileDescriptor> filesToDelete)
        {
            FilesToCopy = filesToCopy;
            FilesToUpdate = filesToUpdate;
            FilesToDelete = filesToDelete;
        }

        public bool Equals(SyncInstructions other)
        {
            bool filesToCopy = CompareLists(other.FilesToCopy, FilesToCopy);
            bool filesToUpdate = CompareLists(other.FilesToUpdate, FilesToUpdate);
            bool filesToDelete = CompareLists(other.FilesToDelete, FilesToDelete);
            return filesToCopy && filesToUpdate && filesToDelete;
        }

        private bool CompareLists(List<FileDescriptor> aList, List<FileDescriptor> bList)
        {
            foreach (FileDescriptor item in aList)
            {
                var bFile = bList.Find(file => file.FileName == item.FileName);
                if (!bList.Any(file => file.FileName==item.FileName))
                {
                    return false;
                }
                if (bFile.FolderPath != item.FolderPath)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
