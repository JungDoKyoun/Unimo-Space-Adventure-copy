using System;

namespace ZL.Unity
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true)]

    public abstract class EnumValueAttribute : Attribute
    {
        public static partial class Cache<TEnum, TEnumValueAttribute>

            where TEnum : Enum

            where TEnumValueAttribute : EnumValueAttribute
        {
            private static TEnumValueAttribute attribute = null;

            public static TEnumValueAttribute Get(TEnum @enum)
            {
                if (attribute == null)
                {
                    var field = @enum.GetType().GetField(@enum.ToString());

                    var attributes = field.GetCustomAttributes(typeof(TEnumValueAttribute), false);

                    attribute = (TEnumValueAttribute)attributes[0];
                }

                return attribute;
            }
        }
    }
}