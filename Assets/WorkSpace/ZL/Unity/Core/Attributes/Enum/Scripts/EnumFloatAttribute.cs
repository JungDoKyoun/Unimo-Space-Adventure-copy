using System;

namespace ZL.Unity
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]

    public sealed class EnumFloatAttribute : EnumValueAttribute
    {
        public readonly float value;

        public EnumFloatAttribute(float value)
        {
            this.value = value;
        }
    }
}