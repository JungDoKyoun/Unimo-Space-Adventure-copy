using System;

namespace ZL.CS
{
    public static partial class EnumEx
    {
        public static int ToInt<TEnum>(this TEnum instance)

            where TEnum : Enum
        {
            return Convert.ToInt32(instance);
        }

        public static ulong ToULong<TEnum>(this TEnum instance)

            where TEnum : Enum
        {
            return Convert.ToUInt64(instance);
        }

        public static TEnum ToEnum<TEnum>(this int instance)

            where TEnum : Enum
        {
            return (TEnum)Enum.ToObject(typeof(TEnum), instance);
        }

        public static bool HasFlag<TEnum>(this TEnum instance, TEnum flag)

            where TEnum : Enum
        {
            return HasFlag(instance.ToULong(), flag.ToULong());
        }

        public static bool HasFlag(this ulong instance, ulong flag)
        {
            return (instance & flag) == flag;
        }

        public static TEnum[] GetValues<TEnum>()

            where TEnum : Enum
        {
            return (TEnum[])Enum.GetValues(typeof(TEnum));
        }
    }
}