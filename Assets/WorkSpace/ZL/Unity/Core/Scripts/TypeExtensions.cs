using System;

using UnityEngine;

namespace ZL.Unity
{
    public static partial class TypeExtensions
    {
        public static bool Belong(this Type instance, params Type[] types)
        {
            foreach (var type in types)
            {
                if (instance == type)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsInheritGeneric(this Type instance, out Type genericType)
        {
            var componentType = typeof(Component);

            while (instance != componentType)
            {
                if (instance.IsGenericType == true)
                {
                    genericType = instance;

                    return true;
                }

                instance = instance.BaseType;
            }

            genericType = null;

            return false;
        }
    }
}