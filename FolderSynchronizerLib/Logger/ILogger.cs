namespace FolderSynchronizerLib
{
    public interface ILogger
    {
        void Write(LogLevel logLevel, string message);
    }
}
