namespace ZL.CS.IO.CSV
{
    public interface ICSVData
    {
        public abstract string GetHeader();

        public abstract void Import(string[] values);

        public abstract string Export();
    }
}