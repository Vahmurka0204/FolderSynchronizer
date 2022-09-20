using System.Text.Json.Serialization;

namespace FolderSynchronizerLib
{
    public class FileDescriptor
    {
        [JsonInclude]
        public string FileName;

        [JsonInclude]
        public string FolderPath;

        [JsonInclude]
        public string Hash;

        public FileDescriptor(string filename, string folderPath, string hash)
        {
            FileName = filename;
            FolderPath = folderPath;
            Hash = hash;
        }

        public FileDescriptor() { }
    }
}
