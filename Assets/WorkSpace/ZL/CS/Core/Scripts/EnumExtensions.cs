using System;

namespace ZL.CS
{
    public static partial class EnumExtensions
    {
        public static int ToInt<TEnum>(this TEnum instance)

            where TEnum : Enum
        {
            var enumUnion = new EnumUnion<TEnum>()
            {
                enumValue = instance,
            };

            unsafe
            {
                int* pointer = &enumUnion.intValue;

                pointer -= 1;

                return *pointer;
            }
        }

        public static TEnum ToEnum<TEnum>(this int instance)

            where TEnum : Enum
        {
            var enumUnion = new EnumUnion<TEnum>();

            unsafe
            {
                int* pointer = &enumUnion.intValue;

                pointer -= 1;

                *pointer = instance;
            }

            return enumUnion.enumValue;
        }

        public static bool Contains<TEnum>(this TEnum instance, TEnum flags)

            where TEnum : Enum
        {
            return instance.ToInt().Contains(flags.ToInt());
        }

        public static bool Contains(this int instance, int flags)
        {
            return (instance & (1 << flags)) != 0;
        }
    }
}