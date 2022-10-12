using System.Collections.Generic;

namespace FolderSynchronizerLib
{
    public interface ISyncManager
    {
        void Copy(List<FileDescriptor> filesToCopy, string path);
        void Delete(List<FileDescriptor> filesToDelete, string path);
        void Update(List<FileDescriptor> filesToUpdate, string path);
    }
}
