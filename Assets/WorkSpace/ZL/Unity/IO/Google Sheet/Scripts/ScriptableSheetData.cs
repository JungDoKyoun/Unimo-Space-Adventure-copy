using GoogleSheetsToUnity;

using System.Collections.Generic;

using UnityEngine;

namespace ZL.Unity.IO.GoogleSheet
{
    public abstract class ScriptableSheetData : ScriptableObject, ISheetData
    {
        public abstract List<string> GetHeader();

        public abstract void Import(GstuSpreadSheet sheet);

        public abstract List<string> Export();
    }
}