using System;

namespace ZL.Unity
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]

    public sealed class EnumBoolAttribute : EnumValueAttribute
    {
        public readonly bool value;

        public EnumBoolAttribute(bool value)
        {
            this.value = value;
        }
    }
}