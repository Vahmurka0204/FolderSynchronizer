using System.Text;

namespace FolderSynchronizerLib
{
    class SummaryLog : ILog
    {
        private int _countOfAdd;
        private int _countOfUpdate;
        private int _countOfDelete;

        public string FormLogToPrint()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"{_countOfAdd} files have been added.");
            stringBuilder.AppendLine($"{_countOfUpdate} files have been updated.");
            stringBuilder.AppendLine($"{_countOfDelete} files have been deleted.");
            stringBuilder.Append("Folders are synchronized.");

            return stringBuilder.ToString();
        }

        public void GetInfoAboutAddFiles(string sourcePath, string destinationPath)
        {
            _countOfAdd++;
        }

        public void GetInfoAboutDeleteFiles(string sourcePath)
        {
            _countOfDelete++;
        }

        public void GetInfoAboutUpdateFiles(string sourcePath, string destinationPath)
        {
            _countOfUpdate++;
        }
    }
}
