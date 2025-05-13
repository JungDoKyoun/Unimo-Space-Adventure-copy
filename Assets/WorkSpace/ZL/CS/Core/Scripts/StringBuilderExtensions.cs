using System.Linq;

using System.Text;

namespace ZL.CS
{
    public static partial class StringBuilderExtensions
    {
        public static string Concat(this StringBuilder instance, params char[] values)
        {
            instance.Capacity += values.Length;

            foreach (var value in values)
            {
                instance.Append(value);
            }

            var @string = instance.ToString();

            return @string;
        }

        public static string Concat(this StringBuilder instance, params string[] values)
        {
            instance.Capacity += values.Sum((value) => value.Length);

            foreach (var value in values)
            {
                instance.Append(value);
            }

            var @string = instance.ToString();

            return @string;
        }
    }
}