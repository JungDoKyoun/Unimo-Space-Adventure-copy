using System;

using System.Collections.Generic;

namespace ZL.CS.Collections
{
    public static class ArrayEx
    {
        public static LinkedList<T> ToLinkedList<T>(this T[] instance)
        {
            var linkedList = new LinkedList<T>();

            CopyTo(instance, ref linkedList);

            return linkedList;
        }

        public static void CopyTo<T>(this T[] instance, ref LinkedList<T> linkedList)
        {
            foreach (var item in instance)
            {
                linkedList.AddLast(item);
            }
        }

        public static T[] Parse<T>(string s, char separator, Func<string, T> Parse)
        {
            var strings = s.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            var result = new T[strings.Length];

            for (int i = 0; i < strings.Length; ++i)
            {
                result[i] = Parse(strings[i]);
            }

            return result;
        }
    }
}