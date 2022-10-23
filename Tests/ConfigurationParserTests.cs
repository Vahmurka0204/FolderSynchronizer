using Xunit;
using FolderSynchronizerLib;
using FolderSynchronizerCLI;

namespace Tests
{
    public class ConfigurationParserTests
    {
        [Theory]
        [InlineData("C:\\", "D:\\")]
        [InlineData("C:\\", "D:\\", "--no-delete")]
        public void CreateInputValidArgs(params string[] args)
        {
            var input = new ConfigurationParser(new TestFolderPathChecker()).Read(args);
        }

        [Theory]
        [InlineData("apple", "|tree:\\", "--no-delete", "-loglevel", "silent")]
        [InlineData("C:\\", "D:\\", "--nodelete")]
        [InlineData("C:\\")]
        [InlineData("C:\\", "D:\\", "-loglevel", "medium", "--no-delete")]
        [InlineData("W<C:\\", "WD>:\\")]
        [InlineData("C:\\", "D:\\", "loglevel", "verbose")]
        [InlineData("C:\\", "D:\\",  "-loglevel", "--no-delete", "silent")]

        public void CreateInputInvalidArgs(params string[] args)
        {
            Assert.Throws<SyncException>(() => {var input = new ConfigurationParser(new TestFolderPathChecker()).Read(args); });
        }

    }
}
