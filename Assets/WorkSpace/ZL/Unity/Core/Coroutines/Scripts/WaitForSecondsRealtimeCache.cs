using System.Collections.Generic;

using UnityEngine;

namespace ZL.Unity.Coroutines
{
    public sealed class WaitForSecondsRealtimeCache
    {
        private static readonly Dictionary<float, WaitForSecondsRealtime> waitForSecondsRealtimeDictionary = new Dictionary<float, WaitForSecondsRealtime>(WaitForSecondsComparerCache.Get());

        public static WaitForSecondsRealtime Get(float seconds)
        {
            if (waitForSecondsRealtimeDictionary.TryGetValue(seconds, out var waitForSecondsRealtime) == false)
            {
                waitForSecondsRealtimeDictionary.Add(seconds, waitForSecondsRealtime = new WaitForSecondsRealtime(seconds));
            }

            return waitForSecondsRealtime;
        }

        public static void Clear()
        {
            waitForSecondsRealtimeDictionary.Clear();
        }
    }
}