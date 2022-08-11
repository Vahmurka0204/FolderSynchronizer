﻿using System.Collections.Generic;

namespace FolderSynchronizerLib
{
    public class FolderSet
    {
        public readonly List<FolderPair> FolderList;
        public readonly bool NoDeleteFlag;
        public readonly LogLevels Loglevel;

        public FolderSet(InputData input)
        {
            var folderWorker = new FolderWorker();

            foreach (var path in input.FoldersPaths)
            {
                var folderPair = new FolderPair(folderWorker.LoadSerializedFolder(path), folderWorker.LoadFolder(path));
                FolderList.Add(folderPair);
            }
                        
            NoDeleteFlag = input.NoDeleteFlag;            
            Loglevel = input.LogLevel;
        }

        public FolderSet() { }
    }
}
