namespace ZL.Unity.Coroutines
{
    public static class WaitForSecondsComparerCache
    {
        private static readonly WaitForSecondsComparer waitForSecondsComparer;

        static WaitForSecondsComparerCache()
        {
            waitForSecondsComparer = new WaitForSecondsComparer();
        }

        public static WaitForSecondsComparer Get()
        {
            return waitForSecondsComparer;
        }
    }
}