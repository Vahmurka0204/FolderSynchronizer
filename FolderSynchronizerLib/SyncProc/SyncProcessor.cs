namespace FolderSynchronizerLib
{
    public class SyncProcessor
    {
        public ISyncManager _syncManager;

        public SyncProcessor()
        {
            _syncManager = new SyncManager();
        }

        public void Synchronize(SyncInstructions syncInstructions, List<string> folderPaths)
        {
            Logger.Info("synchronization started at " + DateTime.Now.ToString());

            foreach (var path in folderPaths)
            {
                var filesToCopy = new List<FileDescriptor>();
                foreach (var f in syncInstructions.FilesToCopy)
                {
                    if (!f.FolderPath.Contains(path))
                    {
                        filesToCopy.Add(new FileDescriptor( f.FileName, f.FolderPath, f.Hash));
                    }
                }
                _syncManager.Copy(filesToCopy, path);
            }

            foreach (var path in folderPaths)
            {
                _syncManager.Update(syncInstructions.FilesToUpdate, path);
            }

            foreach (var path in folderPaths)
            {
                _syncManager.Delete(syncInstructions.FilesToDelete, path);
            }

            Logger.Info("Synchronization ended at " + DateTime.Now.ToString());
        }        
    }
}
