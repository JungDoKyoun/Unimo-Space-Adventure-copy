using UnityEngine;

namespace ZL.Unity.Collections
{
    public static class ArrayEx
    {
        public static void Shuffle<T>(this T[] instance)
        {
            for (int i = instance.Length - 1; i > 0; --i)
            {
                int j = Random.Range(0, i + 1);

                (instance[i], instance[j]) = (instance[j], instance[i]);
            }
        }
    }
}