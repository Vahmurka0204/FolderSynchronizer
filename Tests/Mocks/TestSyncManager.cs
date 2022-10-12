using FolderSynchronizerLib;
using System.Collections.Generic;

// TODO: TestSyncProcessor

namespace Tests
{
    public class TestSyncManager : ISyncManager
    {
        List<FolderSnapshot> _folders;

        public TestSyncManager(List<FolderSnapshot> folders)
        {
            _folders = folders;   
        }
        
        public void Copy(List<FileDescriptor> filesToCopy, string path)
        {
           
        }

        private FileDescriptor GetItemByPath(List<FileDescriptor> files, string path)
        {
            foreach (FileDescriptor file in files)
            {
                if (file.FileName == path)
                {
                    return file;
                }
            }
            return null;
        }

        public void Delete(List<FileDescriptor> filesToDelete, string path)
        {
            
        }

        public void Update(List<FileDescriptor> fileToUpdate, string path)
        {
        }
    }
}
