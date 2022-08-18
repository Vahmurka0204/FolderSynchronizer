namespace FolderSynchronizerLib
{
    public interface ILog
    {
        void GetInfoAboutDeleteFiles(string sourcePath);
        void GetInfoAboutAddFiles(string sourcePath, string destinationPath);
        void GetInfoAboutUpdateFiles(string sourcePath, string destinationPath);
        string FormLogToPrint();
    }
}
