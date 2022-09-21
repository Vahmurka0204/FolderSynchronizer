using System.Text.Json.Serialization;

namespace FolderSynchronizerLib
{
     public class FolderSnapshot
    {
        [JsonInclude]
        public string Path;

        [JsonInclude]
        public List<FileDescriptor> FilesList;
       
        public FolderSnapshot(string address)
        {
            Path = address;            
            FilesList = new List<FileDescriptor>();
        }

        public FolderSnapshot(string path, List<FileDescriptor> fileDescriptors)
        {
            Path = path;
            FilesList = fileDescriptors;
        }

        public FolderSnapshot() { }
    }
}
