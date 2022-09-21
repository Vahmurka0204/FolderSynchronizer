using FolderSynchronizerLib;
using System.Collections.Generic;

// TODO: TestSyncProcessor

namespace Tests
{
    public class TestSyncProcessorManager : ISyncProcessorManager
    {
        List<FolderSnapshot> _folders;

        public TestSyncProcessorManager(List<FolderSnapshot> folders)
        {
            _folders = folders;   
        }
        
        public void Copy(List<FileDescriptor> filesToCopy, string path, ILog log)
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

        public void Delete(List<FileDescriptor> filesToDelete, string path, ILog log)
        {
            
        }

        public void Update(List<FileDescriptor> fileToUpdate, string path, ILog log)
        {
        }
    }
}
