
namespace FolderSynchronizerLib
{
    public class SyncManager : ISyncManager
    {
        public void Copy(List<FileDescriptor> filesToCopy, string destinationFolder)
        {
           foreach(var copyInfo in filesToCopy)
            {
                string source = copyInfo.FolderPath + copyInfo.FileName;
                string destination = destinationFolder + copyInfo.FileName;

                File.Copy(source, destination,true);
                Logger.Info($"Copied from {source} to {destination}.");
            }
        }

        public void Delete(List<FileDescriptor> filesToDelete, string destinationFolder)
        {
            foreach (var deleteInfo in filesToDelete)
            {
                string pathDelete = destinationFolder + deleteInfo.FileName;

                if (!File.Exists(pathDelete))
                {
                    continue;
                }

                File.Delete(pathDelete);
                Logger.Info($"Deleted from {pathDelete}.");
            }
        }

        public void Update(List<FileDescriptor> filesToUpdate, string destinationFolder)
        {
            foreach (var copyInfo in filesToUpdate)
            {
                string source = copyInfo.FolderPath+copyInfo.FileName;
                string destination = destinationFolder + copyInfo.FileName;

                if (!File.Exists(destination) || source==destination)
                {
                    continue;
                }

                File.Copy(source, destination, true);
                Logger.Info($"Copied from {source} to {destination}.");
            }
        }
    }
}
