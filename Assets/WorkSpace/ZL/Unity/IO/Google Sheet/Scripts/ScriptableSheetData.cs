using GoogleSheetsToUnity;

using System.Collections.Generic;

using UnityEngine;

namespace ZL.Unity.IO.GoogleSheet
{
    public abstract class ScriptableSheetData : ScriptableObject, ISheetData
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [ReadOnly(true)]

        private string startCell;

        public string StartCell => startCell;

        public virtual void Import(GstuSpreadSheet sheet)
        {
            string columnIndex = sheet.columns.secondaryKeyLink["name"];

            int rowIndex = sheet.rows.secondaryKeyLink[name];

            startCell = columnIndex + rowIndex;
        }

        public abstract List<string> Export();
    }
}