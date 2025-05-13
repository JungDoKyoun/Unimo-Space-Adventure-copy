using GoogleSheetsToUnity;

using System.Collections.Generic;

namespace ZL.Unity.IO.GoogleSheet
{
    public interface ISheetData
    {
        public string StartCell { get; }

        public void Import(GstuSpreadSheet sheet);

        public List<string> Export();
    }
}