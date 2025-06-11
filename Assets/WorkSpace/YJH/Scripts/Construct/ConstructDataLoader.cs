using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ConstructDataLoader : MonoBehaviour
{
    string downURL = "https://docs.google.com/spreadsheets/d/1ctv3DJMHsrkCvNSFnsjkPqgpFRI48CQ7zBgtVpbyv4Q/export?format=csv&gid=0\r\n";
    public List<ConstructBase> buildings = new List<ConstructBase>();


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

        ParseCSV(www.downloadHandler.text);
    }
    void ParseCSV(string csvText)
    {
        buildings.Clear();
        string[] lines = csvText.Split('\n');

        if (lines.Length < 2)
        {
            Debug.LogWarning("CSV 데이터 없음");
            return;
        }

        for (int i = 1; i < lines.Length; i++) // Skip header
        {
            string line = lines[i].Trim();
            if (string.IsNullOrWhiteSpace(line)) continue;

            string[] tokens = SplitCSVLine(line);
            if (tokens.Length < 5) continue;

            ConstructBase entry = new ConstructBase
            {
                buildID = tokens[0],
                buildName = tokens[1],
                buildingDescription = tokens[2],
                buildRequires = new List<string>(tokens[3].Split(',')),
                buildCosts = ParseBuildCosts(tokens[4])
            };

            buildings.Add(entry);
        }

        Debug.Log($"총 {buildings.Count}개 빌딩 로드됨");
    }

    List<BuildCost> ParseBuildCosts(string costStr)
    {
        List<BuildCost> costs = new List<BuildCost>();
        string[] pairs = costStr.Split(',');

        foreach (string pair in pairs)
        {
            string[] parts = pair.Split(':');
            if (parts.Length == 2 && int.TryParse(parts[1], out int val))
            {
                costs.Add(new BuildCost { key = parts[0], value = val });
            }
        }

        return costs;
    }

    /// CSV 한 줄 파싱 함수 (쉼표/따옴표 처리 포함)
    string[] SplitCSVLine(string line)
    {
        var result = new List<string>();
        bool inQuotes = false;
        string value = "";

        foreach (char c in line)
        {
            if (c == '"')
            {
                inQuotes = !inQuotes;
            }
            else if (c == ',' && !inQuotes)
            {
                result.Add(value);
                value = "";
            }
            else
            {
                value += c;
            }
        }

        result.Add(value);
        return result.ToArray();
    }
}

