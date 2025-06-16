using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RankList : MonoBehaviour
{
    [SerializeField]
    private GameObject rankListPartition;

    [SerializeField]
    private Transform content;


    private void Start()
    {
        
    }

    private void SetRankListPanel()
    {
        for (int i = 0; i < FirebaseDataBaseMgr.TopRankers.Count; i++)
        {
            GameObject newPartition = Instantiate(rankListPartition, content);

            newPartition.name = $"Rank_{i + 1}";

            TextMeshProUGUI tmp = newPartition.GetComponentInChildren<TextMeshProUGUI>();

            // for���� i�� ���� topRanker ����Ʈ�� ������ �ؽ�Ʈ�� ��������
            var (nickname, score) = FirebaseDataBaseMgr.TopRankers[i];

            if (tmp != null)
            {
                tmp.text = $"{i + 1} > {nickname}: {score}";
            }
        }
    }
}
