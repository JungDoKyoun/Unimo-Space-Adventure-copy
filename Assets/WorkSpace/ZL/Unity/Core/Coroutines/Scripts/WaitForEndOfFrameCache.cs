using UnityEngine;

namespace ZL.Unity.Coroutines
{
    public static class WaitForEndOfFrameCache
    {
        private static readonly WaitForEndOfFrame waitForEndOfFrame;

        static WaitForEndOfFrameCache()
        {
            waitForEndOfFrame = new WaitForEndOfFrame();
        }

        public static WaitForEndOfFrame Get()
        {
            return waitForEndOfFrame;
        }
    }
}