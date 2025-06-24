using GoogleSheetsToUnity;

using System.Collections.Generic;

using UnityEngine;

namespace ZL.Unity.SO.GoogleSheet
{
    public abstract class ScriptableGoogleSheetData : ScriptableObject, IGoogleSheetData
    {
        public abstract List<string> GetHeaders();

        public abstract void Import(GstuSpreadSheet sheet);

        public abstract List<string> Export();
    }
}