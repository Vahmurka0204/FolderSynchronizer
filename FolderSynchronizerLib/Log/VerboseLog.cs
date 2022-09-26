using System.Text;

namespace FolderSynchronizerLib
{
    class VerboseLog : ILogger
    {
        List<(string,string)> _addInfo = new List<(string, string)>();
        List<(string, string)> _updateInfo = new List<(string, string)>();
        List<string> _deleteInfo = new List<string>();

        public string FormLogToPrint()
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var pair in _addInfo)
            {
                stringBuilder.AppendLine($"File {pair.Item1} has been copied to {Path.GetDirectoryName(pair.Item2)}");
            }

            foreach (var pair in _updateInfo)
            {
                stringBuilder.AppendLine($"File {pair.Item2} has been updated to {pair.Item1}");
            }

            foreach (var file in _deleteInfo)
            {
                stringBuilder.AppendLine($"File {file} has been deleted");
            }

            stringBuilder.Append("Folders are synchronized.");

            return stringBuilder.ToString();
        }

        public void GetInfoAboutAddFiles(string sourcePath, string destinationPath)
        {
            _addInfo.Add((sourcePath, destinationPath));
        }

        public void GetInfoAboutDeleteFiles(string sourcePath)
        {
            _deleteInfo.Add(sourcePath);
        }

        public void GetInfoAboutUpdateFiles(string sourcePath, string destinationPath)
        {
            _updateInfo.Add((sourcePath, destinationPath));
        }
    }
}
