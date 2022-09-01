using System.Runtime.Serialization.Json;
using System.Text.Json;

namespace FolderSynchronizerLib
{
    public class FolderWorker
    {
        public Folder LoadSerializedFolderAsync(string path)
        {
            string filename = GetFileName(path);

            if (!File.Exists(filename))
            {
                return new Folder(path);
            }


            string jsonFolder = File.ReadAllText(filename);
            
            Folder folder = JsonSerializer.Deserialize<Folder>(jsonFolder);

            return folder;                                
            
        }

        public Folder LoadFolder(string path)
        {
            if (!Directory.Exists(path))
            {
                throw new DirectoryNotFoundException();
            }

            var filesPathList = new List<string>();
            var filesList = new List<FileDescriptor>();
            filesPathList.AddRange(Directory.GetFiles(path));
            var internalFoldersPaths = Directory.GetDirectories(path).ToList();
            int countPath = 0;

            while (countPath < internalFoldersPaths.Count)
            {
                var pathInternalFolder = internalFoldersPaths[countPath];
                filesPathList.AddRange(Directory.GetFiles(pathInternalFolder));
                internalFoldersPaths.AddRange(Directory.GetDirectories(pathInternalFolder));
                countPath++;
            }

            foreach(string filePath in filesPathList)
            {
                FileInfo file = new FileInfo(filePath);
                filesList.Add(new FileDescriptor(GetSubPath(filePath,path), file.GetHashCode()));
            }

            var folder = new Folder(path);
            folder.FilesList = filesList;
            
            return folder;
        }

        private static string GetSubPath(string longPath, string firstPartPath)
        {
            return longPath.Substring(firstPartPath.Length);
        }

        public void SerializeFolder(string path)
        {
            Folder folder = LoadFolder(path);
            var jsonFolder = JsonSerializer.Serialize(folder);
            string filename = GetFileName(path);

            if (File.Exists(filename))
            {
                File.Delete(filename);
            }

            File.WriteAllText(filename, jsonFolder);
        }

        private static string GetFileName(string path)
        {
            string filename = path.Replace("\\", ".");
            filename = filename.Replace(":", "") + ".json";
            return filename;
        }
    }
}
