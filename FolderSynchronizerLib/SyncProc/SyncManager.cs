
namespace FolderSynchronizerLib
{
    public class SyncManager : ISyncManager
    {
        public void Copy(List<FileDescriptor> filesToCopy, string destinationFolder, ILogger logger)
        {
           foreach(var copyInfo in filesToCopy)
            {
                string source = copyInfo.FolderPath + copyInfo.FileName;
                string destination = destinationFolder + copyInfo.FileName;

                File.Copy(source, destination,true);
                logger.GetInfoAboutAddFiles(source, destination);
            }
        }

        public void Delete(List<FileDescriptor> filesToDelete, string destinationFolder, ILogger logger)
        {
            foreach (var deleteInfo in filesToDelete)
            {
                string pathDelete = Path.Combine(destinationFolder, deleteInfo.FileName);

                if (!File.Exists(pathDelete))
                {
                    continue;
                }

                File.Delete(pathDelete);
                logger.GetInfoAboutDeleteFiles(pathDelete);
            }
        }

        public void Update(List<FileDescriptor> filesToUpdate, string destinationFolder, ILogger logger)
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
                logger.GetInfoAboutUpdateFiles(source, destination);
            }
        }
    }
}
