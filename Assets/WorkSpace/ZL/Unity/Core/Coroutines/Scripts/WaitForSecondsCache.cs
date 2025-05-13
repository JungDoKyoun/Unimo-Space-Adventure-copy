using System.Collections.Generic;

using UnityEngine;

namespace ZL.Unity.Coroutines
{
    public static partial class WaitForSecondsCache
    {
        private static readonly Dictionary<float, WaitForSeconds> waitForSecondsDictionary;

        static WaitForSecondsCache()
        {
            waitForSecondsDictionary = new Dictionary<float, WaitForSeconds>(WaitForSecondsComparerCache.Get());
        }

        public static WaitForSeconds Get(float seconds)
        {
            if (waitForSecondsDictionary.TryGetValue(seconds, out var waitForSeconds) == false)
            {
                waitForSecondsDictionary.Add(seconds, waitForSeconds = new WaitForSeconds(seconds));
            }

            return waitForSeconds;
        }

        public static void Clear()
        {
            waitForSecondsDictionary.Clear();
        }
    }
}