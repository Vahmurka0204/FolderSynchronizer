namespace FolderSynchronizerLib 
{ 
    public class Logger
    {
        static private List<ILogger> loggers = new List<ILogger>();

        public static void AddLogger(ILogger logger) { loggers.Add(logger); }

        private static void Write(LogLevel logLevel, string message)
        {
            foreach (var logger in loggers)
            {
                logger.Write(logLevel, message);
            }
        }
        
        public static void Info(string message)
        {
            Write(LogLevel.Info, message);
        }

        public static void Debug(string message)
        {
            Write(LogLevel.Debug, message);
        }
    }
}
