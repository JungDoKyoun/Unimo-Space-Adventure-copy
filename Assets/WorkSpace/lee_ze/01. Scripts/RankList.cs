using Google.GData.AccessControl;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RankList : MonoBehaviour
{
    [SerializeField]
    private GameObject rankListPartition;

    private Transform content;

    private Button rankUpdateButton;

    private void OnEnable()
    {
        content = transform.Find("Scroll View/Viewport/Content").GetComponent<Transform>();

        rankUpdateButton = transform.Find("Rank Update Button").GetComponent<Button>();

        rankUpdateButton.onClick.AddListener(OnClickUpdateRankList);
    }

    private IEnumerator Start()
    {
        // Firebase 연결 대기
        yield return new WaitUntil(() => FirebaseAuthMgr.IsFirebaseReady == true);

        yield return new WaitUntil(() => FirebaseDataBaseMgr.IsDataBaseReady == true);

        StartCoroutine(SetRankListPanel());
    }

    private void OnDisable()
    {
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
        yield return FirebaseDataBaseMgr.Instance.UpdateRank();

        yield return new WaitUntil(predicate: () => FirebaseDataBaseMgr.IsRankUpdated == true);

        yield return SetRankListPanel();
    }

    private IEnumerator SetRankListPanel()
    {
        yield return new WaitUntil(predicate: () => FirebaseDataBaseMgr.IsRankUpdated == true);

        if (FirebaseDataBaseMgr.TopRankers.Count == 0)
        {
            Debug.LogWarning("TopRankers 데이터가 없습니다.");

            yield break;
        }

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
