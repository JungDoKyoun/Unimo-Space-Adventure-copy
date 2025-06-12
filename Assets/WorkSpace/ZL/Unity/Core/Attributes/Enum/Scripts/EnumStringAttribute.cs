using System;

namespace ZL.Unity
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]

    public sealed class EnumStringAttribute : EnumValueAttribute
    {
        public readonly string value;

        public EnumStringAttribute(string value)
        {
            this.value = value;
        }
    }
}