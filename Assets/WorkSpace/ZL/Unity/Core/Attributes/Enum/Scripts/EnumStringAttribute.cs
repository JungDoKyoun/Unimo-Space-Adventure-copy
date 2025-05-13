namespace ZL.Unity
{
    public sealed class EnumStringAttribute : EnumValueAttribute
    {
        public readonly string value;

        public EnumStringAttribute(string value)
        {
            this.value = value;
        }
    }
}