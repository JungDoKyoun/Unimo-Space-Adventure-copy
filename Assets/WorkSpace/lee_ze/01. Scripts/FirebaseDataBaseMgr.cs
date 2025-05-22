using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using TMPro;

public class FirebaseDataBaseMgr : MonoBehaviour
{
    public static FirebaseDataBaseMgr Instance { get; private set; }

    DatabaseReference dbRef;

    FirebaseUser user;

    [Header("Save")]
    [SerializeField]
    private TMP_InputField rewardIngameCurrencyField;

    [SerializeField]
    private TMP_InputField rewardMetaCurrencyField;

    [Header("Display")]
    [SerializeField]
    private TextMeshProUGUI rewardIngameCurrencyText;

    [SerializeField]
    private TextMeshProUGUI rewardMetaCurrencyText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);

            return;
        }
        else
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);
        }
    }

    private IEnumerator Start()
    {
        yield return new WaitUntil(() => FirebaseAuthMgr.IsFirebaseReady == true);

        Debug.Log("user 대기중");

        yield return new WaitUntil(() => FirebaseAuthMgr.user != null);

        Debug.Log(FirebaseAuthMgr.IsFirebaseReady);

        this.dbRef = FirebaseAuthMgr.dbRef;

        this.user = FirebaseAuthMgr.user;

        StartCoroutine(ShowUserIngameCurrency());

        StartCoroutine(ShowUserMetaCurrency());
    }

    #region Currency management

    // >>>>>>>>> Ingame Currency

    private IEnumerator ShowUserIngameCurrency()
    {
        var getTask = dbRef.Child("users").Child(user.UserId).Child(user.DisplayName).Child("rewardIngameCurrency").GetValueAsync();

        yield return new WaitUntil(predicate: () => getTask.IsCompleted);

        if (getTask.Result.Exists == true && int.TryParse(getTask.Result.Value.ToString(), out int savedValue))
        {
            // 여기에 디스플레이

            rewardIngameCurrencyText.text = savedValue.ToString();
        }
    }

    public void SaveCurrencyInDataBase() // 전체 재화 저장 >> 버튼 이벤트 함수로 호출
    {
        if (rewardIngameCurrencyField != null) StartCoroutine(UpdateRewardIngameCurrency(int.Parse(rewardIngameCurrencyField.text))); // reward를 인자값으로 주면 해당 값을 더하게 해야됨.
        else Debug.Log("Ingame empty");

        if (rewardMetaCurrencyField != null) StartCoroutine(UpdateRewardMetaCurrency(int.Parse(rewardMetaCurrencyField.text)));
        else Debug.Log("Meta empty");
    }

    // 게임 클리어 실패 시 인게임 재화 초기화
    public IEnumerator InitIngameCurrency()
    {
        var getTask = dbRef.Child("users").Child(user.UserId).Child(user.DisplayName).Child("rewardIngameCurrency").SetValueAsync(0);

        yield return new WaitUntil(predicate: () => getTask.IsCompleted);
    }

    /// <summary>
    /// 인자값: 재화 추가 시 >> 양수 | 재화 사용 시 >> 음수
    /// </summary>
    /// <param name="ingameCurrencyToAdd"></param>
    /// <returns></returns>
    public IEnumerator UpdateRewardIngameCurrency(int ingameCurrencyToAdd) // Ingame 재화 저장 함수(더할 값)
    {
        int tempIngameCurrency = 0;

        var getTask = dbRef.Child("users").Child(user.UserId).Child(user.DisplayName).Child("rewardIngameCurrency").GetValueAsync(); // 현재 인게임 재화 불러오기

        yield return new WaitUntil(predicate: () => getTask.IsCompleted);

        if (getTask.Exception != null) // 불러오기 실패 시 나가기
        {
            Debug.LogWarning($"[Get] reason : {getTask.Exception}");

            yield break;
        }

        if (getTask.Result.Exists == true && int.TryParse(getTask.Result.Value.ToString(), out int savedValue)) // string으로 불러온 ingame currency를 tryparse로 savedValue에 저장
        {
            tempIngameCurrency = savedValue; // tempIngameCurrency = 기존 재화
        }

        int newIngameCurrency = tempIngameCurrency + ingameCurrencyToAdd; // 재화 최신화

        var DBTask = dbRef.Child("users").Child(user.UserId).Child(user.DisplayName).Child("rewardIngameCurrency").SetValueAsync(newIngameCurrency); // 최신화 된 재화 DB 저장

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"reason : {DBTask.Exception}");
        }

        else
        {
            // 업데이트 된 재화 디스플레이
            StartCoroutine(ShowUserIngameCurrency());
        }
    }


    // >>>>>>>>> Meta Currency

    private IEnumerator ShowUserMetaCurrency()
    {
        var getTask = dbRef.Child("users").Child(user.UserId).Child(user.DisplayName).Child("rewardMetaCurrency").GetValueAsync();

        yield return new WaitUntil(predicate: () => getTask.IsCompleted);

        if (getTask.Result.Exists == true && int.TryParse(getTask.Result.Value.ToString(), out int savedValue))
        {
            // 여기에 디스플레이

            rewardMetaCurrencyText.text = savedValue.ToString();
        }
    }

    /// <summary>
    /// 인자값: 재화 추가 시 >> 양수 | 재화 사용 시 >> 음수
    /// </summary>
    /// <param name="metaCurrencyToAdd"></param>
    /// <returns></returns>
    public IEnumerator UpdateRewardMetaCurrency(int metaCurrencyToAdd) // Meta 재화 저장 함수(더할 값)
    {
        int tempMetaCurrency = 0;

        var getTask = dbRef.Child("users").Child(user.UserId).Child(user.DisplayName).Child("rewardMetaCurrency").GetValueAsync(); // 현재 메타 재화 불러오기

        yield return new WaitUntil(predicate: () => getTask.IsCompleted);

        if (getTask.Exception != null)
        {
            Debug.LogWarning($"[Get] reason : {getTask.Exception}");

            yield break;
        }

        if (getTask.Result.Exists == true && int.TryParse(getTask.Result.Value.ToString(), out int savedValue)) // string으로 불러온 meta currency를 tryparse로 savedValue에 저장
        {
            tempMetaCurrency = savedValue; // tempMetaCurrency = 기존 재화
        }

        int newMetaCurrency = tempMetaCurrency + metaCurrencyToAdd; // 재화 최신화

        var DBTask = dbRef.Child("users").Child(user.UserId).Child(user.DisplayName).Child("rewardMetaCurrency").SetValueAsync(newMetaCurrency);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"reason : {DBTask.Exception}");
        }

        else
        {
            // 업데이트 된 재화 디스플레이
            StartCoroutine(ShowUserMetaCurrency());
        }
    }

    #endregion

    #region Tile management


    


    #endregion
}
