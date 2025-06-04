#if UNITY_EDITOR

using System.IO;

using UnityEditor;

using UnityEngine;

namespace ZL.Unity
{
    public static partial class ScriptableObjectEx
    {
        public static TScriptableObject CreateAndSaveAsset<TScriptableObject>(string folderPath)

            where TScriptableObject : ScriptableObject
        {
            var scriptableObject = ScriptableObject.CreateInstance<TScriptableObject>();

            if (Directory.Exists(folderPath) == false)
            {
                Directory.CreateDirectory(folderPath);
            }

            for (int i = 1; ; ++i)
            {
                var fileName = typeof(TScriptableObject).Name.ToTitleCase() + $" {i}.asset";

                var filePath = Path.Combine(folderPath, fileName);

                if (File.Exists(filePath) == false)
                {
                    AssetDatabase.CreateAsset(scriptableObject, filePath);

                    break;
                }
            }

            AssetDatabase.SaveAssets();

            return scriptableObject;
        }
    }
}

#endif