using UnityEngine;

namespace ZL.Unity.Coroutines
{
    public static class WaitForFixedUpdateCache
    {
        private static readonly WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

        public static WaitForFixedUpdate Get()
        {
            return waitForFixedUpdate;
        }
    }
}