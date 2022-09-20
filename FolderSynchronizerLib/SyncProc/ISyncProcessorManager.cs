using System.Collections.Generic;

namespace FolderSynchronizerLib
{
    public interface ISyncProcessorManager
    {
        void Copy(List<FileDescriptor> fileToCopy, string path, ILog log);
        void Delete(List<FileDescriptor> fileToDelete, string path, ILog log);
        void Update(List<FileDescriptor> fileToUpdate, string path, ILog log);
    }
}
