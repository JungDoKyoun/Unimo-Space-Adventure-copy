using System;

namespace ZL.CS
{
    public struct EnumUnion<TEnum>

        where TEnum : Enum
    {
        public TEnum enumValue;

        public int intValue;
    }
}