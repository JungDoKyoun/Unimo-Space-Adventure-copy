using GoogleSheetsToUnity;

using System;

using System.Collections.Generic;

using UnityEngine;

using UnityObject = UnityEngine.Object;

namespace ZL.Unity.IO.GoogleSheet
{
    public abstract class ScriptableGoogleSheet<TGoogleSheetData> : ScriptableObject

        where TGoogleSheetData : UnityObject, IGoogleSheetData
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

        public TGoogleSheetData[] Datas
        {
            get => datas;
        }

        private Dictionary<string, TGoogleSheetData> dataDictionary = null;

        public Dictionary<string, TGoogleSheetData> DataDictionary
        {
            get
            {
                if (dataDictionary == null)
                {
                    dataDictionary = new Dictionary<string, TGoogleSheetData>(datas.Length);

                    foreach (var data in datas)
                    {
                        dataDictionary.Add(data.name, data);
                    }
                }

                return dataDictionary;
            }
        }

        private int requestCount = 0;

        public void Read()
        {
            SpreadsheetManager.Read(sheetConfig.GetSearch(), OnReadSuccessful, containsMergedCells);
        }

        private void OnReadSuccessful(GstuSpreadSheet sheet)
        {
            for (int i = 0; i < datas.Length; ++i)
            {
                datas[i].Import(sheet);
            }

            dataDictionary = null;

            FixedDebug.Log($"Successfully read '{name}' from Google sheet.");
        }

        public void Write()
        {
            string column = sheetConfig.TitleColumn;

            int row = sheetConfig.TitleRow;

            requestCount = datas.Length + 1;

            SpreadsheetManager.Write(sheetConfig.GetSearch($"{column}{row++}"), new ValueRange(datas[0].GetHeader()), OnWriteSuccessful);

            for (int i = 0; i < datas.Length; ++i)
            {
                var data = datas[i];

                SpreadsheetManager.Write(sheetConfig.GetSearch($"{column}{row++}"), new ValueRange(data.Export()), OnWriteSuccessful);
            }
        }

        private void OnWriteSuccessful()
        {
            if (--requestCount == 0)
            {
                FixedDebug.Log($"Successfully written '{name}' to Google sheet.");
            }
        }
    }
}