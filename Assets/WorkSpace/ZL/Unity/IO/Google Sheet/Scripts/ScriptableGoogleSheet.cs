using GoogleSheetsToUnity;

using System;

using System.IO;

#if UNITY_EDITOR

using UnityEditor;

#endif

using UnityEngine;

using ZL.Unity.Collections;

namespace ZL.Unity.IO.GoogleSheet
{
    public abstract class ScriptableGoogleSheet<TGoogleSheetData> : ScriptableGoogleSheet<string, TGoogleSheetData>

        where TGoogleSheetData : ScriptableObject, IGoogleSheetData
    {
        protected override string GeyDataKey(TGoogleSheetData data)
        {
            return data.name;
        }
    }

    public abstract class ScriptableGoogleSheet<TKey, TGoogleSheetData> : ScriptableObject

        where TGoogleSheetData : ScriptableObject, IGoogleSheetData
    {
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

        [Button("ClearDatas")]

        [Button("LoadAllDatasAtPath")]

        private bool containsMergedCells = false;

        [SerializeField]

        protected TGoogleSheetData[] datas = null;

        public TGoogleSheetData[] Datas
        {
            get => datas;
        }

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Button(nameof(SerializeDatas))]

        protected SerializableDictionary<TKey, TGoogleSheetData> dataDictionary = null;

        public TGoogleSheetData this[TKey key]
        {
            get => dataDictionary[key];
        }

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

        public void ClearDatas()
        {
            datas = new TGoogleSheetData[0];

            EditorUtility.SetDirty(this);
        }

        public void LoadAllDatasAtPath()
        {
            datas = AssetDatabaseEx.LoadAllAssetsAtPath<TGoogleSheetData>(DirectoryPath);

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
                var data = datas[i];

                data.Import(sheet);

                FixedEditorUtility.SetDirty(data);
            }

            SerializeDatas();

            FixedDebug.Log($"Successfully read '{name}' from Google sheet.");
        }

        public virtual void SerializeDatas()
        {
            dataDictionary.Clear();

            for (int i = 0; i < datas.Length; ++i)
            {
                var data = datas[i];

                dataDictionary.Add(GeyDataKey(data), data);
            }

            FixedEditorUtility.SetDirty(this);
        }

        protected abstract TKey GeyDataKey(TGoogleSheetData data);

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
            FixedEditorUtility.SetDirty(this);

            FixedDebug.Log($"Successfully written '{name}' to Google sheet.");
        }
    }
}