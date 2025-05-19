using GoogleSheetsToUnity;

using UnityEngine;

namespace ZL.Unity.IO.GoogleSheet
{
    public abstract class ScriptableSheet<TSheetData> : ScriptableObject

        where TSheetData : ISheetData
    {
        [Space]

        [SerializeField]

        private ScriptableSheetConfig sheetConfig = null;

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

        private TSheetData[] datas = null;

        public void Read()
        {
            SpreadsheetManager.Read(sheetConfig.GetSearch(), ImportAllDatas, containsMergedCells);
        }

        private void ImportAllDatas(GstuSpreadSheet sheet)
        {
            for (int i = 0; i < datas.Length; ++i)
            {
                datas[i].Import(sheet);
            }
        }

        public void Write()
        {
            var column = sheetConfig.TitleColumn;

            int row = sheetConfig.TitleRow;

            SpreadsheetManager.Write(sheetConfig.GetSearch($"{column}{row++}"), new ValueRange(datas[0].GetHeader()), null);

            for (int i = 0; i < datas.Length; ++i)
            {
                var data = datas[i];

                SpreadsheetManager.Write(sheetConfig.GetSearch($"{column}{row++}"), new ValueRange(data.Export()), null);

            }
        }
    }
}