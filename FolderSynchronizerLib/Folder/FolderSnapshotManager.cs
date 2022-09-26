using System.Security.Cryptography;
using System.Text.Json;

namespace FolderSynchronizerLib
{
    public class FolderSnapshotManager : IFolderSnapshotManager
    {
        public FolderSnapshot DeserializeFolderSnapshot(string path)
        {
            string filename = GetFileName(path);

            if (!File.Exists(filename))
            {
                return new FolderSnapshot(path);
            }


            string jsonFolder = File.ReadAllText(filename);

            FolderSnapshot folderSnapshot = JsonSerializer.Deserialize<FolderSnapshot>(jsonFolder);

            return folderSnapshot;

        }

        public FolderSnapshot MakeFolderSnapshot(string path)
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

            foreach (string filePath in filesPathList)
            {
                filesList.Add(new FileDescriptor(GetSubPath(filePath, path), path, CalculateMD5(filePath)));
            }

            var folderSnapshot = new FolderSnapshot(path);
            folderSnapshot.FilesList = filesList;

            return folderSnapshot;
        }

        private static string GetSubPath(string longPath, string firstPartPath)
        {
            return longPath.Substring(firstPartPath.Length);
        }

        public void SerializeFolderSnapshot(string path)
        {
            FolderSnapshot folderSnapshot = MakeFolderSnapshot(path);
            var jsonFolder = JsonSerializer.Serialize(folderSnapshot);
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

        public static string CalculateMD5(string fileName)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(fileName))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }
    }
}
