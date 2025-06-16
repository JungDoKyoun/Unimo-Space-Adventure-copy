using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RankList : MonoBehaviour
{
    [SerializeField]
    private GameObject rankListPartition;

    [SerializeField]
    private Transform content;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private IEnumerator Start()
    {
        // Firebase ���� ���
        yield return new WaitUntil(() => FirebaseAuthMgr.IsFirebaseReady == true);

        // �α��� ���
        yield return new WaitUntil(() => FirebaseAuthMgr.User != null);

        UpdateRankListPanel();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateRankListPanel();
    }

    private void UpdateRankListPanel()
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

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
