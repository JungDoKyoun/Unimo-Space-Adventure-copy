using System;

using System.Collections.Generic;

using System.Reflection;

namespace ZL.Unity
{
    public abstract class EnumValueAttribute : Attribute
    {
        public static partial class Cache<TEnum, TEnumValueAttribute>

            where TEnum : Enum

            where TEnumValueAttribute : EnumValueAttribute
        {
            private static readonly Dictionary<TEnum, TEnumValueAttribute> attributes = new Dictionary<TEnum, TEnumValueAttribute>();

            public static TEnumValueAttribute Get(TEnum @enum)
            {
                if (attributes.ContainsKey(@enum) == false)
                {
                    var field = @enum.GetType().GetField(@enum.ToString());

                    var attribute = field.GetCustomAttribute<TEnumValueAttribute>(false);

                    attributes.Add(@enum, attribute);
                }

                return attributes[@enum];
            }
        }
    }
}