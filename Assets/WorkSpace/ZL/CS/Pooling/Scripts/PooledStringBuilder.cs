using System.Text;

namespace ZL.CS.Pooling
{
    public static partial class PooledStringBuilder
    {
        public static string Concat(params char[] values)
        {
            var stringBuilder = Generate();

            var @string = stringBuilder.Concat(values);

            Collect(stringBuilder);

            return @string;
        }

        public static string Concat(params string[] values)
        {
            var stringBuilder = Generate();

            var @string = stringBuilder.Concat(values);

            Collect(stringBuilder);

            return @string;
        }

        public static StringBuilder Generate(int capacity)
        {
            var stringBuilder = Generate();

            stringBuilder.Capacity = capacity;

            return stringBuilder;
        }

        public static StringBuilder Generate()
        {
            var stringBuilder = ClassPool<StringBuilder>.Generate();

            return stringBuilder;
        }

        public static string Collect(StringBuilder value)
        {
            var @string = value.ToString();

            ClassPool<StringBuilder>.Collect(value.Clear());

            return @string;
        }
    }
}