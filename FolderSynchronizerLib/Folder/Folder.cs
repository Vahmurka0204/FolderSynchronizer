using System.Text.Json.Serialization;

namespace FolderSynchronizerLib
{
     public class Folder
    {
        [JsonInclude]
        public string Path;

        [JsonInclude]
        public List<FileDescriptor> FilesList;
       
        public Folder(string address)
        {
            Path = address;            
            FilesList = new List<FileDescriptor>();
        }

        public Folder(string path, List<FileDescriptor> fileDescriptors)
        {
            Path = path;
            FilesList = fileDescriptors;
        }

        public Folder() { }
    }
}
