using GoogleSheetsToUnity;

using System;

using System.Collections.Generic;

using System.IO;

#if UNITY_EDITOR

using UnityEditor;

#endif

using UnityEngine;

using ZL.CS.Singleton;

namespace ZL.Unity.IO.GoogleSheet
{
    public abstract class ScriptableGoogleSheet<TScriptableGoogleSheet, TGoogleSheetData> : ScriptableObject, ISingleton<TScriptableGoogleSheet>

        where TScriptableGoogleSheet : ScriptableGoogleSheet<TScriptableGoogleSheet, TGoogleSheetData>

        where TGoogleSheetData : ScriptableObject, IGoogleSheetData
    {
        public static TScriptableGoogleSheet Instance
        {
            get => ISingleton<TScriptableGoogleSheet>.Instance;
        }

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Button("CreateNewConfig")]

        private ScriptableGoogleSheetConfig sheetConfig = null;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [PropertyField]

        [Margin]

        [Button(nameof(Read))]

        [Button(nameof(Write))]

        [Margin]

        [Button("AddNewData")]

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

        #if UNITY_EDITOR

        private string DirectoryPath
        {
            get
            {
                var assetPath = AssetDatabase.GetAssetPath(this);

                return Path.GetDirectoryName(assetPath);
            }
        }

        public void CreateNewConfig()
        {
            sheetConfig = CreateInstance<ScriptableGoogleSheetConfig>();

            AssetDatabaseEx.CreateAsset(sheetConfig, DirectoryPath, name + " Config");

            AssetDatabase.SaveAssets();

            EditorUtility.SetDirty(this);
        }

        public void AddNewData()
        {
            var data = CreateInstance<TGoogleSheetData>();

            Array.Resize(ref datas, datas.Length + 1);

            datas[^1] = data;

            AssetDatabaseEx.CreateAsset(data, DirectoryPath, 1);

            AssetDatabase.SaveAssets();

            EditorUtility.SetDirty(this);
        }

        #endif

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

            EditorUtility.SetDirty(this);

            FixedDebug.Log($"Successfully read '{name}' from Google sheet.");
        }

        public void Write()
        {
            var inputData = new ValueRange(datas[0].GetHeaders());

            for (int i = 0; i < datas.Length; ++i)
            {
                inputData.Add(datas[i].Export());
            }
            
            SpreadsheetManager.Write(sheetConfig.GetSearch(), inputData, OnWriteSuccessful);
        }

        private void OnWriteSuccessful()
        {
            EditorUtility.SetDirty(this);

            FixedDebug.Log($"Successfully written '{name}' to Google sheet.");
        }
    }
}