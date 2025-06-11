using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class ConstructDataLoader //: MonoBehaviour
{
    [MenuItem("Tools/UpdateConstructData")]
    public static void UpdateConstructData()
    {
        string csvPath = "Assets/WorkSpace/YJH/CSV/Construct.csv";
        if (!File.Exists(csvPath))
        {
            Debug.LogError("CSV 파일이 존재하지 않습니다: " + csvPath);
            return;
        }

        string[] lines = File.ReadAllLines(csvPath);
        string[] headers = lines[0].Split(',');

        // 컬럼 인덱스 파악
        int idIndex = System.Array.IndexOf(headers, "buildID");
        int nameIndex = System.Array.IndexOf(headers, "buildName");
        int descIndex = System.Array.IndexOf(headers, "buildingDescription");

        string[] guids = AssetDatabase.FindAssets("t:ConstructBase");
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            ConstructBase data = AssetDatabase.LoadAssetAtPath<ConstructBase>(path);

            // CSV에서 해당 ID와 일치하는 줄 찾기
            for (int i = 1; i < lines.Length; i++)
            {
                string[] fields = lines[i].Split(',');

                if (fields[idIndex] == data.buildID)
                {
                    data.buildName = fields[nameIndex];
                    data.buildingDescription = fields[descIndex];

                    EditorUtility.SetDirty(data); // 에셋에 변경사항 반영
                    Debug.Log($"Updated: {data.buildID}");
                    break;
                }
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }


   
}
