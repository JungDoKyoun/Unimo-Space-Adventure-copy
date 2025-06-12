using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class ConstructDataLoader : MonoBehaviour
{
    string downURL = "https://docs.google.com/spreadsheets/d/1ctv3DJMHsrkCvNSFnsjkPqgpFRI48CQ7zBgtVpbyv4Q/export?format=csv&gid=0\r\n";
    public List<ConstructBase> buildings = new List<ConstructBase>();
    [SerializeField] ConstructManager constructManager;

    // Start is called before the first frame update
    void Start()
    {
        //buildinfs=ConstructManager.Instance.
        StartCoroutine(LoadCSVFile());
    }
    private IEnumerator LoadCSVFile()
    {
        UnityWebRequest www = UnityWebRequest.Get(downURL);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("CSV 다운로드 실패: " + www.error);
            yield break;
        }

        ApplyCSVToConstructBases(www.downloadHandler.text);
    }
    void ApplyCSVToConstructBases(string csvText)
    {
        string[] lines = csvText.Split('\n');
        if (lines.Length < 2) return;

        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i].Trim();
            if (string.IsNullOrEmpty(line)) continue;

            string[] tokens = SplitCSVLine(line);
            if (tokens.Length < 5) continue;

            string buildID = tokens[0];
            string buildName = tokens[1];
            string buildingDescription = tokens[2];
            List<string> buildRequires = tokens[3].Split(',').Select(s => s.Trim()).ToList();
            List<BuildCost> buildCosts = ParseBuildCosts(tokens[4]);

            if (constructManager.AllBuildingDic.TryGetValue(buildID, out var target))
            {
                target.buildName = buildName;
                target.buildingDescription = buildingDescription;
                target.buildRequires = buildRequires;
                target.buildCosts = buildCosts;

                Debug.Log($"[업데이트 완료] {buildID}");
            }
            else
            {
                Debug.LogWarning($"[경고] buildID '{buildID}'를 가진 건물 없음");
            }
        }
    }

    List<BuildCost> ParseBuildCosts(string input)
    {
        var result = new List<BuildCost>();
        var entries = input.Split(',');

        foreach (var entry in entries)
        {
            var kv = entry.Split(':');
            if (kv.Length == 2 && int.TryParse(kv[1], out int val))
            {
                result.Add(new BuildCost { key = kv[0].Trim(), value = val });
            }
        }

        return result;
    }

    string[] SplitCSVLine(string line)
    {
        List<string> result = new List<string>();
        bool inQuotes = false;
        string current = "";

        foreach (char c in line)
        {
            if (c == '"') inQuotes = !inQuotes;
            else if (c == ',' && !inQuotes)
            {
                result.Add(current);
                current = "";
            }
            else current += c;
        }

        result.Add(current);
        return result.ToArray();
    }
}

