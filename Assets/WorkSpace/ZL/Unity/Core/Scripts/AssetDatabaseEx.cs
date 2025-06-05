#if UNITY_EDITOR

using Google.GData.Extensions;
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
    }
}

#endif