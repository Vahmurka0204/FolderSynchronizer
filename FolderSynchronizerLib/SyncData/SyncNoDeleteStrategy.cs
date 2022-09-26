using System.Collections.Generic;

namespace FolderSynchronizerLib
{
    public class SyncNoDeleteStrategy : ISyncStrategy
    {
        private List<FileDescriptor> _filesForAdding = new List<FileDescriptor>();
        private List<FileDescriptor> _filesForUpdate = new List<FileDescriptor>();
        private List<FileDescriptor> _filesForDelete = new List<FileDescriptor>();

        public SyncInstructions MakeSyncInstruction(FolderSet folderSet)
        {
           foreach (var folderPair in folderSet.FolderList)
            {
                _filesForAdding = FindNewFiles(folderPair, _filesForAdding);
                _filesForDelete = FindDeleteFiles(folderPair, _filesForDelete);
                _filesForUpdate = FindUpdateFiles(folderPair, _filesForUpdate);
            }

            var syncInstruction = new SyncInstructions(_filesForAdding, _filesForUpdate, new List<FileDescriptor>());

            return syncInstruction;
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
                    if (_filesForUpdate.Any(item => item.FileName == oldFile.FileName))
                    {
                        continue;
                    }

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

                if (oldFile == null && !filesToAdd.Any(item => item.FileName==newFile.FileName))
                {
                    filesToAdd.Add(new FileDescriptor(newFile.FileName, newFolderSnapshot.Path, newFile.Hash));
                }
            }

            return filesToAdd;
        }

        private List<FileDescriptor> FindUpdateFiles(FolderPair folderPair, List<FileDescriptor> filesToUpdate)
        {
            var newFolderSnapshot = folderPair.New;
            var oldFolderSnapshot = folderPair.Old;

            foreach (var newFile in newFolderSnapshot.FilesList)
            {
                var oldFile = GetItemByPath(oldFolderSnapshot, newFile.FileName);

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

        private static FileDescriptor GetItemByPath(FolderSnapshot folder, string path)
        {
            foreach (FileDescriptor file in folder.FilesList)
            {
                if (file.FileName == path)
                {
                    return file;
                }
            }
            return null;
        }        
    }
}
