using System;

namespace ZL.Unity
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]

    public sealed class EnumIntAttribute : EnumValueAttribute
    {
        public readonly int value;

        public EnumIntAttribute(int value)
        {
            this.value = value;
        }
    }
}