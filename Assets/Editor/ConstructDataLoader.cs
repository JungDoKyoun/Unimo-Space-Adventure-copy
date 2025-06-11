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
            Debug.LogError("CSV ������ �������� �ʽ��ϴ�: " + csvPath);
            return;
        }

        string[] lines = File.ReadAllLines(csvPath);
        string[] headers = lines[0].Split(',');

        // �÷� �ε��� �ľ�
        int idIndex = System.Array.IndexOf(headers, "buildID");
        int nameIndex = System.Array.IndexOf(headers, "buildName");
        int descIndex = System.Array.IndexOf(headers, "buildingDescription");

        string[] guids = AssetDatabase.FindAssets("t:ConstructBase");
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            ConstructBase data = AssetDatabase.LoadAssetAtPath<ConstructBase>(path);

            // CSV���� �ش� ID�� ��ġ�ϴ� �� ã��
            for (int i = 1; i < lines.Length; i++)
            {
                string[] fields = lines[i].Split(',');

                if (fields[idIndex] == data.buildID)
                {
                    data.buildName = fields[nameIndex];
                    data.buildingDescription = fields[descIndex];

                    EditorUtility.SetDirty(data); // ���¿� ������� �ݿ�
                    Debug.Log($"Updated: {data.buildID}");
                    break;
                }
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }


   
}
