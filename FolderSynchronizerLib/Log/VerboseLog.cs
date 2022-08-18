using System.Text;

namespace FolderSynchronizerLib
{
    class VerboseLog : ILog
    {
        List<(string,string)> _addInfoList = new List<(string, string)>();
        List<(string, string)> _updateInfoList = new List<(string, string)>();
        List<string> _deleteInfoList = new List<string>();

        public string FormLogToPrint()
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var pair in _addInfoList)
            {
                stringBuilder.AppendLine($"File {pair.Item1} has been copied to {Path.GetDirectoryName(pair.Item2)}");
            }

            foreach (var pair in _updateInfoList)
            {
                stringBuilder.AppendLine($"File {pair.Item2} has been updated to {pair.Item1}");
            }

            foreach (var file in _deleteInfoList)
            {
                stringBuilder.AppendLine($"File {file} has been deleted");
            }

            return stringBuilder.ToString();
        }

        public void GetInfoAboutAddFiles(string sourcePath, string destinationPath)
        {
            _addInfoList.Add((sourcePath, destinationPath));
        }

        public void GetInfoAboutDeleteFiles(string sourcePath)
        {
            _deleteInfoList.Add(sourcePath);
        }

        public void GetInfoAboutUpdateFiles(string sourcePath, string destinationPath)
        {
            _updateInfoList.Add((sourcePath, destinationPath));
        }
    }
}
