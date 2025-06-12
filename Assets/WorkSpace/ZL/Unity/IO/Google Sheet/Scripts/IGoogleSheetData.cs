using GoogleSheetsToUnity;

using System.Collections.Generic;

namespace ZL.Unity.IO.GoogleSheet
{
    public interface IGoogleSheetData
    {
        public List<string> GetHeaders();

        public void Import(GstuSpreadSheet sheet);

        public List<string> Export();
    }
}