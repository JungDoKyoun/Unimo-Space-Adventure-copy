using GoogleSheetsToUnity;

using UnityEngine;

namespace ZL.Unity.IO.GoogleSheet
{
    public abstract class ScriptableGoogleSheet<TGoogleSheetData> : ScriptableObject

        where TGoogleSheetData : IGoogleSheetData
    {
        [Space]

        [SerializeField]

        private ScriptableGoogleSheetConfig sheetConfig = null;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [PropertyField]

        [Margin]

        [Button(nameof(Read))]

        [Button(nameof(Write))]

        private bool containsMergedCells = false;

        [Space]

        [SerializeField]

        private TGoogleSheetData[] datas = null;

        private int requestCount = 0;

        public void Read()
        {
            SpreadsheetManager.Read(sheetConfig.GetSearch(), OnRead, containsMergedCells);
        }

        private void OnRead(GstuSpreadSheet sheet)
        {
            for (int i = 0; i < datas.Length; ++i)
            {
                datas[i].Import(sheet);
            }

            FixedDebug.Log($"Successfully read '{name}' from Google sheet.");
        }

        public void Write()
        {
            string column = sheetConfig.TitleColumn;

            int row = sheetConfig.TitleRow;

            requestCount = datas.Length + 1;

            SpreadsheetManager.Write(sheetConfig.GetSearch($"{column}{row++}"), new ValueRange(datas[0].GetHeader()), OnWrite);

            for (int i = 0; i < datas.Length; ++i)
            {
                var data = datas[i];

                SpreadsheetManager.Write(sheetConfig.GetSearch($"{column}{row++}"), new ValueRange(data.Export()), OnWrite);
            }
        }

        private void OnWrite()
        {
            if (--requestCount == 0)
            {
                FixedDebug.Log($"Successfully written '{name}' to Google sheet.");
            }
        }
    }
}