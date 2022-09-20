
namespace FolderSynchronizerLib
{
    public class SyncProcessorManager : ISyncProcessorManager
    {
        public void Copy(List<FileDescriptor> filesToCopy, string destinationFolder, ILog log)
        {
           foreach(var copyInfo in filesToCopy)
            {
                string source = copyInfo.FolderPath + copyInfo.FileName;
                string destination = destinationFolder + copyInfo.FileName;

                File.Copy(source, destination,true);
                log.GetInfoAboutAddFiles(source, destination);
            }
        }

        public void Delete(List<FileDescriptor> filesToDelete, string destinationFolder, ILog log)
        {
            foreach (var deleteInfo in filesToDelete)
            {
                string pathDelete = Path.Combine(destinationFolder, deleteInfo.FileName);

                if (!File.Exists(pathDelete))
                {
                    continue;
                }

                File.Delete(pathDelete);
                log.GetInfoAboutDeleteFiles(pathDelete);
            }
        }

        public void Update(List<FileDescriptor> filesToUpdate, string destinationFolder, ILog log)
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
                log.GetInfoAboutUpdateFiles(source, destination);
            }
        }
    }
}
