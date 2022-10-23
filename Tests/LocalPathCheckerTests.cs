using FolderSynchronizerCLI;
using Xunit;

namespace Tests
{
    public class LocalPathCheckerTests
    {
        [Theory]
        [InlineData("C:\\")]
        public void LocalPathCheckerValid(params string[] path)
        {
            Assert.True(new LocalPathChecker().IsValid(path[0]));
        }

        [Theory]
        [InlineData("k:\\")]
        public void LocalPathCheckerInvalid(params string[] path)
        {
            Assert.False(new LocalPathChecker().IsValid(path[0]));
        }
    }
}
