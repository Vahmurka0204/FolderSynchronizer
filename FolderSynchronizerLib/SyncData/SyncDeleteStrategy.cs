namespace FolderSynchronizerLib
{
    public class SyncDeleteStrategy : ISyncStrategy
    {
        private static List<FileDescriptor> _filesForAdding = new List<FileDescriptor> ();
        private static List<FileDescriptor> _filesForUpdate = new List<FileDescriptor>();
        private static List<FileDescriptor> _filesForDelete = new List<FileDescriptor>();

        public SyncInstructions MakeSyncInstruction(FolderSet folderSet)
        {
            foreach (var folderPair in folderSet.FolderList)
            {
                _filesForAdding = FindNewFiles(folderPair, _filesForAdding);
                _filesForUpdate = FindUpdateFiles(folderPair, _filesForUpdate);
                _filesForDelete = FindDeleteFiles(folderPair, _filesForDelete);
            }

            _filesForUpdate = RemoveCollision(_filesForDelete, _filesForUpdate);
                       
            return new SyncInstructions(_filesForAdding, _filesForUpdate, _filesForDelete);
        }

        private List<FileDescriptor> FindDeleteFiles(FolderPair folderPair, List<FileDescriptor> filesToDelete)
        {
            var newFolderSnapshot = folderPair.New;
            var oldFolderSnapshot = folderPair.Old;

            foreach (var oldFile in oldFolderSnapshot.FilesList)
            {
                var newFile = GetItemByPath(newFolderSnapshot, oldFile.FileName);

                if (newFile == null && !filesToDelete.Any(item => item.FileName==oldFile.FileName))
                {
                    filesToDelete.Add(new FileDescriptor( oldFile.FileName, oldFolderSnapshot.Path, oldFile.Hash));
                }
            }

            return filesToDelete;
        }

        private List<FileDescriptor> FindNewFiles(FolderPair folderPair, List<FileDescriptor> filesToAdd)
        {
            var newFolderSnapshot = folderPair.New;
            var oldFolderSnapshot = folderPair.Old;

            foreach (var newFile in newFolderSnapshot.FilesList)
            {
                var oldFile = GetItemByPath(oldFolderSnapshot, newFile.FileName);

                if (oldFile == null && !filesToAdd.Any(item=> item.FileName == newFile.FileName))
                {
                    filesToAdd.Add(new FileDescriptor(newFile.FileName, newFolderSnapshot.Path, newFile.Hash));
                }
            }

            return filesToAdd;
        }

        private List<FileDescriptor> FindUpdateFiles(FolderPair folderPair, List<FileDescriptor> filesToUpdate)
        {
            var newFolderSnapshot = folderPair.New;
            var oldFolderSnaphot = folderPair.Old;

            foreach (var newFile in newFolderSnapshot.FilesList)
            {
                var oldFile = GetItemByPath(oldFolderSnaphot, newFile.FileName);

                if (oldFile == null)
                {
                    continue;
                }

                bool haveDifferentContent = (newFile.Hash != oldFile.Hash);

                if (haveDifferentContent && !filesToUpdate.Any(item => item.FileName == newFile.FileName) && !_filesForDelete.Any(item => item.FileName == newFile.FileName))
                {
                    filesToUpdate.Add(new FileDescriptor(newFile.FileName, newFolderSnapshot.Path, newFile.Hash));
                }
            }

            return filesToUpdate;
        }

        private FileDescriptor GetItemByPath(FolderSnapshot folderSnapShot, string path)
        {
            foreach (FileDescriptor file in folderSnapShot.FilesList)
            {
                if (file.FileName == path)
                {
                    return file;
                }
            }
            return null;
        }

        private List<FileDescriptor> RemoveCollision(List<FileDescriptor> filesToDelete, List<FileDescriptor> filesToUpdate)
        {
            foreach (var file in filesToDelete)
            {
                if (filesToUpdate.Any(item=> item.FileName==file.FileName))
                {
                    filesToUpdate.Remove(file);
                }
            }

            return filesToUpdate;
        }
    }
}
