using System.Collections.Generic;

namespace FolderSynchronizerLib
{
    public class SyncDataReaderNoDeleteStrategy : ISyncDataReaderStrategy
    {
        private List<FileDescriptor> _filesForAddList = new List<FileDescriptor>();
        private List<FileDescriptor> _filesForUpdateList = new List<FileDescriptor>();
        private List<FileDescriptor> _filesForDeleteList = new List<FileDescriptor>();

        public SyncInstructions MakeSyncData(FolderSet folderSet)
        {
           foreach (var folderPair in folderSet.FolderList)
            {
                _filesForAddList = FindNewFiles(folderPair, _filesForAddList);
                _filesForDeleteList = FindDeleteFiles(folderPair, _filesForDeleteList);
                _filesForUpdateList = FindUpdateFiles(folderPair, _filesForUpdateList);
            }

            var syncData = new SyncInstructions(_filesForAddList, _filesForUpdateList, new List<FileDescriptor>());

            return syncData;
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
                    if (_filesForUpdateList.Any(item => item.FileName == oldFile.FileName))
                    {
                        continue;
                    }

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

                if (oldFile == null && !addList.Any(item => item.FileName==newFile.FileName))
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
