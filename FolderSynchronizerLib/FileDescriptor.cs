using System.Text.Json.Serialization;

namespace FolderSynchronizerLib
{
    public class FileDescriptor
    {
        [JsonInclude]
        public string Path;

        [JsonInclude]
        public string Hash;

        public FileDescriptor(string filePath, string hash)
        {
            Path = filePath;
            Hash = hash;
        }

        public FileDescriptor() { }
    }
}
