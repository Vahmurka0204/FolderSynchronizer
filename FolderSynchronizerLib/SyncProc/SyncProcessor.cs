namespace FolderSynchronizerLib
{
    public class SyncProcessor
    {
        public ISyncProcessorManager _syncProcManager;

        public SyncProcessor()
        {
            _syncProcManager = new SyncProcessorManager();
        }

        public void Synchronize(SyncData syncData, List<string> folderPaths, ILog log)
        {
            foreach (var path in folderPaths)
            {
                var filesToCopy = new List<FileDescriptor>();
                foreach (var f in syncData.FilesToCopy)
                {
                    if (!f.FolderPath.Contains(path))
                    {
                        filesToCopy.Add(new FileDescriptor( f.FileName, f.FolderPath, f.Hash));
                    }
                }
                _syncProcManager.Copy(filesToCopy, path, log);
            }

            foreach (var path in folderPaths)
            {
                _syncProcManager.Update(syncData.FilesToUpdate, path, log);
            }

            foreach (var path in folderPaths)
            {
                _syncProcManager.Delete(syncData.FilesToDelete, path, log);
            }
        }        
    }
}
