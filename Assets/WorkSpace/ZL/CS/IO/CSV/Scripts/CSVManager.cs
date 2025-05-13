using System.IO;

using System.Text;

using ZL.CS.Pooling;

namespace ZL.CS.IO.CSV
{
    public static partial class CSVManager
    {
        public static bool TryLoad<TCSVData>(string path, out TCSVData[] datas)

            where TCSVData : ICSVData, new()
        {
            if (File.Exists(path) == false)
            {
                datas = null;

                return false;
            }

            var lines = File.ReadAllLines(path);

            datas = new TCSVData[lines.Length - 1];

            for (int i = 1; i < lines.Length; ++i)
            {
                var data = new TCSVData();

                data.Import(lines[i].Split(','));

                datas[i - 1] = data;
            }

            return true;
        }

        public static void Save<TCSVData>(string path, TCSVData[] datas)

            where TCSVData : ICSVData, new()
        {
            var stringBuilder = PooledStringBuilder.Generate();

            stringBuilder.Append(datas[0].GetHeader());

            stringBuilder.AppendLine();

            for (int i = 0; i < datas.Length; ++i)
            {
                stringBuilder.Append(datas[i].Export());

                stringBuilder.AppendLine();
            }

            File.WriteAllText(path, PooledStringBuilder.Collect(stringBuilder), Encoding.UTF8);
        }
    }
}