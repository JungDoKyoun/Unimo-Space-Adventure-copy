using System;

using ZL.CS;

namespace ZL.Unity.IO
{
    [Serializable]

    public sealed class EnumPref<TEnum> : SerializablePlayerPref<TEnum>

        where TEnum : Enum
    {
        public EnumPref(string key, TEnum value) : base(key, value)
        {

        }

        public override void LoadValue()
        {
            Value = GetInt(key).ToEnum<TEnum>();
        }

        public override void SaveValue()
        {
            SetInt(key, value.ToInt());
        }
    }
}