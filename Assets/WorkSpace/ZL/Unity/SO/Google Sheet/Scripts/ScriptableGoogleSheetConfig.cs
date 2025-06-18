using GoogleSheetsToUnity;

using UnityEngine;

namespace ZL.Unity.SO.GoogleSheet
{
    [CreateAssetMenu(menuName = "ZL/Google Sheet/Google Sheet Config", fileName = "Google Sheet Config")]

    public sealed class ScriptableGoogleSheetConfig : ScriptableObject
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        private string sheetId = "";

        public string SheetID
        {
            get => sheetId;
        }

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        private string worksheetName = "";

        public string WorksheetName
        {
            get => worksheetName;
        }

        [Space]

        [SerializeField]

        private string startCell = "A1";

        public string StartCell
        {
            get => startCell;
        }

        [SerializeField]

        private string endCell = "Z100";

        public string EndCell
        {
            get => endCell;
        }

        [SerializeField]

        private string titleColumn = "A";

        public string TitleColumn
        {
            get => titleColumn;
        }

        [SerializeField]

        private int titleRow = 1;

        public int TitleRow
        {
            get => titleRow;
        }

        public GSTU_Search GetSearch()
        {
            return new GSTU_Search(sheetId, worksheetName, startCell, endCell, titleColumn, titleRow);
        }

        public GSTU_Search GetSearch(string startCell)
        {
            return new GSTU_Search(sheetId, worksheetName, startCell);
        }
    }
}