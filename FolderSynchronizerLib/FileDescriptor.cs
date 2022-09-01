using System.Text.Json.Serialization;

namespace FolderSynchronizerLib
{
    public class FileDescriptor
    {
        [JsonInclude]
        public string Path;

        [JsonInclude]
        public int Hash;

        public FileDescriptor(string filePath, int hash)
        {
            Path = filePath;
            Hash = hash;
        }

        public FileDescriptor() { }
    }
}
