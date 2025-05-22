using GoogleSheetsToUnity;

using System.Collections.Generic;

using UnityEngine;

namespace ZL.Unity.IO.GoogleSheet
{
    public abstract class ScriptableGoogleSheetData : ScriptableObject, IGoogleSheetData
    {
        public abstract List<string> GetHeader();

        public abstract void Import(GstuSpreadSheet sheet);

        public abstract List<string> Export();
    }
}