namespace ZL.Unity
{
    public sealed class EnumFloatAttribute : EnumValueAttribute
    {
        public readonly float value;

        public EnumFloatAttribute(float value)
        {
            this.value = value;
        }
    }
}