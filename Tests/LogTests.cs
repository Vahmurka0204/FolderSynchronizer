using Xunit;
using FolderSynchronizerLib;

namespace Tests
{
    public class LogTests
    {
        [Fact]
        public void SummaryLogTest()
        {
            var logCreator = new LogCreator();
            var log = logCreator.Create(LogLevels.Summary);
            for (int i = 0;i<5;i++)
                log.GetInfoAboutAddFiles("","");
            for (int i = 0; i < 3; i++)
                log.GetInfoAboutUpdateFiles("", "");
            for (int i = 0; i < 7; i++)
                log.GetInfoAboutDeleteFiles("");

            Assert.Equal("5 files have been added.\r\n3 files have been updated.\r\n7 files have been deleted.\r\nFolders are synchronized.", log.FormLogToPrint());
        }
    }
}
