namespace FolderSynchronizerLib
{
    public class StrategyFactory
    {
        public ISyncStrategy Create(bool noDeleteFlag)
        {
            if (noDeleteFlag)
            {
                return new SyncNoDeleteStrategy();
            }
            else
            {
                return new SyncDeleteStrategy();
            }
        }
    }
}
