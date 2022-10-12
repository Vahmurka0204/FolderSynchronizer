namespace FolderSynchronizerLib
{
    public class ConsoleLogger: ILogger
    {
        public void Write(LogLevel logLevel, string message)
        {
            Console.WriteLine($"{logLevel} {message}");
        }
    }
}
