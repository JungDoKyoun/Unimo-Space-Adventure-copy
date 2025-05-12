using GoogleSheetsToUnity;

using UnityEngine;

namespace ZL.Unity.IO.GoogleSheet
{
    [CreateAssetMenu(menuName = "ZL/Google Sheet/Sheet Config", fileName = "Sheet Config")]

    public sealed class SheetConfig : ScriptableObject
    {
        [Space]

        [SerializeField]

        private string sheetId = "";

        public string SheetID => sheetId;

        [SerializeField]

        private string worksheetName = "Sheet";

        public string WorksheetName => worksheetName;

        [SerializeField]

        private string startCell = "A1";

        public string StartCell => startCell;

        [SerializeField]

        private string endCell = "Z100";

        public string EndCell => endCell;

        [SerializeField]

        private string titleColumn = "A";

        public string TitleColumn => titleColumn;

        [SerializeField]

        private int titleRow = 1;

        public int TitleRow => titleRow;

        public GSTU_Search GetSearch()
        {
            return new GSTU_Search(sheetId, worksheetName, startCell, endCell, titleColumn, titleRow);
        }

        public GSTU_Search GetSearch(ScriptableSheetData data)
        {
            return new GSTU_Search(sheetId, worksheetName, data.StartCell);
        }
    }
}