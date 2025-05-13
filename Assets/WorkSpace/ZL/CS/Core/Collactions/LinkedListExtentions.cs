using System.Collections.Generic;

namespace ZL.CS.Collections
{
    public static partial class LinkedListExtentions
    {
        public static T PopFirst<T>(this LinkedList<T> instance)
        {
            var value = instance.First.Value;

            instance.RemoveFirst();

            return value;
        }

        public static T PopLast<T>(this LinkedList<T> instance)
        {
            var value = instance.Last.Value;

            instance.RemoveLast();

            return value;
        }
    }
}