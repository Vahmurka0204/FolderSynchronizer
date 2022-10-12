namespace FolderSynchronizerLib
{
    public class FileLogger: ILogger
    {
        private string _fileName;

        public FileLogger(string fileName)
        {
            _fileName = fileName;
        }
        public void Write(LogLevel logLevel, string message) 
        {

            using (var stream = new StreamWriter(_fileName, true))
            {
                stream.WriteLine($"{logLevel} {message}");
            }
        }
    }
}
