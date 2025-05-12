using System;

using System.Collections;

using System.Reflection;

namespace ZL.Unity.Collections
{
    public static partial class EnumeratorExtensions
    {
        public static IEnumerator Clone(this IEnumerator instance)
        {
            var type = instance.GetType();

            var underlyingSystemType = type.UnderlyingSystemType;

            var constructor = underlyingSystemType.GetConstructor(new Type[] { typeof(int) });

            var clone = (IEnumerator)constructor.Invoke(new object[] { 0 });

            var fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var field in fields)
            {
                var value = field.GetValue(instance);

                field.SetValue(clone, value);
            }

            fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);

            foreach (var field in fields)
            {
                var value = field.GetValue(instance);

                field.SetValue(clone, value);
            }

            return clone;
        }
    }
}