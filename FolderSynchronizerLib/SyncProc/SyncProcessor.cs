using System.Collections.Generic;

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
                var filesToCopy = new Dictionary<string, string>();
                foreach (var f in syncData.FilesToCopy)
                {
                    if (!f.Value.Contains(path))
                    {
                        filesToCopy.Add(f.Key, f.Value);
                    }
                }
                _syncProcManager.Copy(filesToCopy, path, log);
            }

            foreach (var path in folderPaths)
            {
                _syncProcManager.Update(syncData.FilesToCopy, path, log);
            }

            foreach (var path in folderPaths)
            {
                _syncProcManager.Delete(syncData.FilesToDelete, path, log);
            }
        }        
    }
}
