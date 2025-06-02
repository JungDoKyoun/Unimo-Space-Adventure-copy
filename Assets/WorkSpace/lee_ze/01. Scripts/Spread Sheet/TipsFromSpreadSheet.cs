using System.Collections.Generic;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine;
using TMPro;

// Matching Canvas에 들어가있음
public class TipsFromSpreadSheet : MonoBehaviour
{
    private readonly string ADDRESS = "https://docs.google.com/spreadsheets/d/1231BLcYGttF61TDv2BWnKyUDi_ziXN1d";

    private readonly string RANGE = "B2:D";

    private readonly long SHEET_ID = 219602961;

    public string[] tips;

    [SerializeField]
    private TextMeshProUGUI gameTip;

    private void Start()
    {
        StartCoroutine(LoadData());
    }

    private void ShowTipWhenReady()
    {
        if (gameTip == null)
        {
            gameTip = GameObject.Find("Matching Canvas")?.transform.Find("Under Bar Panel/Game Tips").GetComponent<TextMeshProUGUI>();
        }
        else
        {
            gameTip.text = "Tip \n\n" + tips[Random.Range(0, tips.Length)];
        }
    }

    private IEnumerator LoadData()
    {
        string url = GetTSVAdress(ADDRESS, RANGE, SHEET_ID);

        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Error: {request.error}");

            yield break;
        }

        // 배열에 팁 담기
        string rawText = request.downloadHandler.text;

        string[] lines = rawText.Split('\n');

        List<string> tipsList = new List<string>();

        foreach (string line in lines)
        {
            string trimmed = line.Trim();

            if (!string.IsNullOrEmpty(trimmed))
            {
                tipsList.Add(trimmed);
            }
        }

        tips = tipsList.ToArray();

        ShowTipWhenReady();
    }

    private static string GetTSVAdress(string address, string range, long sheetID)
    {
        return $"{address}/export?format=tsv&range={range}&gid={sheetID}";
    }
}