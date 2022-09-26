using System.Collections.Generic;

namespace FolderSynchronizerLib
{
    public interface ISyncManager
    {
        void Copy(List<FileDescriptor> filesToCopy, string path, ILogger logger);
        void Delete(List<FileDescriptor> filesToDelete, string path, ILogger logger);
        void Update(List<FileDescriptor> filesToUpdate, string path, ILogger logger);
    }
}
