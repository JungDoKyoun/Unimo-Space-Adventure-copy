using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Google.GData.AccessControl;

public class RankList : MonoBehaviour
{
    [SerializeField]
    private GameObject rankListPartition;

    private Transform content;

    private Button rankUpdateButton;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        content = transform.Find("Scroll View/Viewport/Content").GetComponent<Transform>();

        rankUpdateButton = transform.Find("Rank Update Button").GetComponent<Button>();

        rankUpdateButton.onClick.AddListener(OnClickUpdateRankList);
    }

    private IEnumerator Start()
    {
        // Firebase 연결 대기
        yield return new WaitUntil(() => FirebaseAuthMgr.IsFirebaseReady == true);

        StartCoroutine(SetRankListPanel());
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(SetRankListPanel());
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        rankUpdateButton.onClick.RemoveListener(OnClickUpdateRankList);
    }

    // 코루틴 함수(UpdateRankListPanel)는 OnClick 연결 불가해서 간접 연결을 위해 만든 함수
    private void OnClickUpdateRankList()
    {
        StartCoroutine(UpdateRankListPanel());
    }

    // 업데이트 버튼에 할당 할 함수
    private IEnumerator UpdateRankListPanel()
    {
        StartCoroutine(FirebaseDataBaseMgr.Instance.UpdateRank());

        yield return new WaitForSeconds(1f);

        yield return new WaitUntil(predicate: () => FirebaseDataBaseMgr.IsRankUpdated == true);

        StartCoroutine(SetRankListPanel());
    }

    private IEnumerator SetRankListPanel()
    {
        yield return new WaitUntil(predicate: () => FirebaseDataBaseMgr.IsRankUpdated == true);

        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < FirebaseDataBaseMgr.TopRankers.Count; i++)
        {
            GameObject newPartition = Instantiate(rankListPartition, content);

            newPartition.name = $"Rank_{i + 1}";

            TextMeshProUGUI tmp = newPartition.GetComponentInChildren<TextMeshProUGUI>();

            // for문의 i에 따른 topRanker 리스트의 정보를 텍스트로 가져오기
            var (nickname, score) = FirebaseDataBaseMgr.TopRankers[i];
                
            if (tmp != null)
            {
                tmp.text = $"{i + 1} > {nickname}: {score}";
            }
        }

        // bool값 초기화
        FirebaseDataBaseMgr.IsRankUpdated = false;
    }
}
