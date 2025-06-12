#if UNITY_EDITOR

using System.IO;

using UnityEditor;

using UnityEngine;

namespace ZL.Unity
{
    public static class AssetDatabaseEx
    {
        public static void CreateAsset<TObject>(TObject asset, string directoryPath, int startNumber)

            where TObject : Object
        {
            CreateAsset(asset, directoryPath, null, startNumber);
        }

        public static void CreateAsset<TObject>(TObject asset, string directoryPath, string fileName = null, int startNumber = -1)

            where TObject : Object
        {
            fileName ??= typeof(TObject).Name.ToTitleCase();

            var filePath = Path.Combine(directoryPath, fileName);

            if (startNumber < 0)
            {
                if (TryCreateAsset(asset, filePath + ".asset") == true)
                {
                    return;
                }

                startNumber = 1;
            }

            for (int number = startNumber; ; ++number)
            {
                if (TryCreateAsset(asset, filePath + $" {number}.asset") == true)
                {
                    break;
                }
            }
        }

        public static bool TryCreateAsset<TObject>(TObject asset, string path)

            where TObject : Object
        {
            if (File.Exists(path) == true)
            {
                return false;
            }

            AssetDatabase.CreateAsset(asset, path);

            return true;
        }

        public static TObject[] LoadAllAssetsAtPath<TObject>(string directoryPath)

            where TObject : Object
        {
            string filter = $"t:{typeof(TObject).Name}";

            string[] guids = FindAssets(filter, directoryPath);

            TObject[] assets = new TObject[guids.Length];

            for (int i = 0; i < guids.Length; ++i)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);

                assets[i] = AssetDatabase.LoadAssetAtPath<TObject>(assetPath);
            }

            return assets;
        }

        public static string[] FindAssets(string filter, params string[] searchInFolders)
        {
            return AssetDatabase.FindAssets(filter, searchInFolders);
        }
    }
}

#endif