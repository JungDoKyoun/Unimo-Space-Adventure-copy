using System.Collections.Generic;

using System.Linq;

using UnityEngine;

namespace ZL.Unity
{
    public static partial class RandomEx
    {
        public static bool DrawLots(float probability = 0.5f)
        {
            return Random.value <= probability;
        }

        public static T Range<T>(T[] array)
        {
            return array[Random.Range(0, array.Length)];
        }

        public static T Range<T>(List<T> list)
        {
            return list[Random.Range(0, list.Count)];
        }

        public static LinkedListNode<T> Range<T>(LinkedList<T> linkedList)
        {
            int index = Random.Range(0, linkedList.Count);

            var node = linkedList.First;

            for (int i = 0; i < index; ++i)
            {
                node = node.Next;
            }

            return node;
        }

        public static Vector3 Range(in Vector3 minInclusive, in Vector3 maxInclusive)
        {
            return new Vector3()
            {
                x = Random.Range(minInclusive.x, maxInclusive.x),

                y = Random.Range(minInclusive.y, maxInclusive.y),

                z = Random.Range(minInclusive.z, maxInclusive.z)
            };
        }

        public static Vector3 EulerAngles()
        {
            return new Vector3()
            {
                x = Random.Range(0f, 360f),

                y = Random.Range(0f, 360f),

                z = Random.Range(0f, 360f)
            };
        }
    }
}