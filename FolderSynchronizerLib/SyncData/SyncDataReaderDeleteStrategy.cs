using System.Collections.Generic;

namespace FolderSynchronizerLib
{
    public class SyncDataReaderDeleteStrategy : ISyncDataReaderStrategy
    {
        private static List<FileDescriptor> _filesForAddingList = new List<FileDescriptor> ();
        private static List<FileDescriptor> _filesForUpdateList = new List<FileDescriptor>();
        private static List<FileDescriptor> _filesForDeleteList = new List<FileDescriptor>();

        public SyncInstructions MakeSyncData(FolderSet folderSet)
        {
            foreach (var folderPair in folderSet.FolderList)
            {
                _filesForAddingList = FindNewFiles(folderPair, _filesForAddingList);
                _filesForUpdateList = FindUpdateFiles(folderPair, _filesForUpdateList);
                _filesForDeleteList = FindDeleteFiles(folderPair, _filesForDeleteList);
            }

            _filesForUpdateList = RemoveCollision(_filesForDeleteList, _filesForUpdateList);
                       
            return new SyncInstructions(_filesForAddingList, _filesForUpdateList, _filesForDeleteList);
        }

        private List<FileDescriptor> FindDeleteFiles(FolderPair folderPair, List<FileDescriptor> deleteList)
        {
            var newFolder = folderPair.New;
            var oldFolder = folderPair.Old;

            foreach (var oldFile in oldFolder.FilesList)
            {
                var newFile = GetItemByPath(newFolder, oldFile.FileName);

                if (newFile == null && !deleteList.Any(item => item.FileName==oldFile.FileName))
                {
                    deleteList.Add(new FileDescriptor( oldFile.FileName, oldFolder.Path, oldFile.Hash));
                }
            }

            return deleteList;
        }

        private List<FileDescriptor> FindNewFiles(FolderPair folderPair, List<FileDescriptor> addList)
        {
            var newFolder = folderPair.New;
            var oldFolder = folderPair.Old;

            foreach (var newFile in newFolder.FilesList)
            {
                var oldFile = GetItemByPath(oldFolder, newFile.FileName);

                if (oldFile == null && !addList.Any(item=> item.FileName == newFile.FileName))
                {
                    addList.Add(new FileDescriptor(newFile.FileName, newFolder.Path, newFile.Hash));
                }
            }

            return addList;
        }

        private List<FileDescriptor> FindUpdateFiles(FolderPair folderPair, List<FileDescriptor> updateList)
        {
            var newFolder = folderPair.New;
            var oldFolder = folderPair.Old;

            foreach (var newFile in newFolder.FilesList)
            {
                var oldFile = GetItemByPath(oldFolder, newFile.FileName);

                if (oldFile == null)
                {
                    continue;
                }

                bool haveDifferentContent = (newFile.Hash != oldFile.Hash);

                if (haveDifferentContent && !updateList.Any(item => item.FileName == newFile.FileName) && !_filesForDeleteList.Any(item => item.FileName == newFile.FileName))
                {
                    updateList.Add(new FileDescriptor(newFile.FileName, newFolder.Path, newFile.Hash));
                }
            }

            return updateList;
        }

        private FileDescriptor GetItemByPath(FolderSnapshot folder, string path)
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
