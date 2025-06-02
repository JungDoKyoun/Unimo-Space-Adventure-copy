using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using TMPro;
using UnityEngine.SceneManagement;

public class FirebaseDataBaseMgr : MonoBehaviour
{
    public static FirebaseDataBaseMgr Instance { get; private set; }

    DatabaseReference dbRef;

    FirebaseUser user;

    [Header("Display")]
    [SerializeField]
    private TextMeshProUGUI rewardIngameCurrencyText;

    [SerializeField]
    private TextMeshProUGUI rewardMetaCurrencyText;

    private static int ingameCurrency;

    private static int metaCurrency;

    private static int bluePrint;

    private static float winningRate;

    private static float playCount;

    private static float winCount;

    #region properties

    public static float WinningRate
    {
        get => winningRate;

        private set
        {
            winningRate = value;
        }
    }

    public static float PlayCount
    {
        get => playCount;

        private set
        {
            playCount = value;
        }
    }

    public static float WinCount
    {
        get => winCount;

        private set
        {
            winCount = value;
        }
    }

    public static int IngameCurrency
    {
        get => ingameCurrency;

        private set
        {
            ingameCurrency = value;
        }
    }

    public static int MetaCurrency
    {
        get => metaCurrency;

        private set
        {
            metaCurrency = value;
        }
    }

    public static int Blueprint
    {
        get => bluePrint;

        private set
        {
            bluePrint = value;
        }
    }

    #endregion

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
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private IEnumerator Start()
    {
        yield return new WaitUntil(() => FirebaseAuthMgr.IsFirebaseReady == true);

        yield return new WaitUntil(() => FirebaseAuthMgr.User != null);

        this.dbRef = FirebaseAuthMgr.dbRef;

        this.user = FirebaseAuthMgr.User;

        StartCoroutine(ShowUserIngameCurrency());

        StartCoroutine(ShowUserMetaCurrency());

        StartCoroutine(UpdateWinningRate());
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) // 씬 바뀔 때 마다 수행되는 것
    {
        if (user != null)
        {
            // 재화 업데이트
            StartCoroutine(ShowUserIngameCurrency());

            StartCoroutine(ShowUserMetaCurrency());
        }
    }

    #region Currency management

    // >>>>>>>>> Ingame Currency <<<<<<<<<

    public IEnumerator InitIngameCurrency() // 게임 클리어 실패 시 인게임 재화 초기화
    {
        var getTask = dbRef.Child("users").Child(user.UserId).Child(user.DisplayName).Child("rewardIngameCurrency").SetValueAsync(0);

        yield return new WaitUntil(predicate: () => getTask.IsCompleted);
    }

    private IEnumerator ShowUserIngameCurrency()
    {
        var getTask = dbRef.Child("users").Child(user.UserId).Child(user.DisplayName).Child("rewardIngameCurrency").GetValueAsync();

        yield return new WaitUntil(predicate: () => getTask.IsCompleted);

        if (getTask.Result.Exists == true && int.TryParse(getTask.Result.Value.ToString(), out int savedValue))
        {
            // 여기에 디스플레이
            rewardIngameCurrencyText = GameObject.Find("Reward Ingame Currency")?.GetComponent<TextMeshProUGUI>();

            if(rewardIngameCurrencyText != null) rewardIngameCurrencyText.text = savedValue.ToString();
        }
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

    // >>>>>>>>> Meta Currency <<<<<<<<<

    private IEnumerator ShowUserMetaCurrency()
    {
        var getTask = dbRef.Child("users").Child(user.UserId).Child(user.DisplayName).Child("rewardMetaCurrency").GetValueAsync();

        yield return new WaitUntil(predicate: () => getTask.IsCompleted);

        if (getTask.Result.Exists == true && int.TryParse(getTask.Result.Value.ToString(), out int savedValue))
        {
            // 여기에 디스플레이
            rewardMetaCurrencyText = GameObject.Find("Reward Meta Currency")?.GetComponent<TextMeshProUGUI>();

            if (rewardIngameCurrencyText != null) rewardMetaCurrencyText.text = savedValue.ToString();
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

        // MetaCurrency 캐싱
        MetaCurrency = newMetaCurrency;
    }

    // >>>>>>>>> BluePrint <<<<<<<<<

    /// <summary>
    /// 인자값: 재화 추가 시 >> 양수 | 재화 사용 시 >> 음수
    /// </summary>
    /// <param name="bluePrintToAdd"></param>
    /// <returns></returns>
    public IEnumerator UpdateRewardBluePrint(int bluePrintToAdd) // Blue Print 재화 저장 함수(더할 값)
    {
        int tempBluePrint = 0;

        var getTask = dbRef.Child("users").Child(user.UserId).Child(user.DisplayName).Child("rewardBluePrint").GetValueAsync(); // 현재 설계도 불러오기

        yield return new WaitUntil(predicate: () => getTask.IsCompleted);

        if (getTask.Exception != null) // 불러오기 실패 시 나가기
        {
            Debug.LogWarning($"[Get] reason : {getTask.Exception}");

            yield break;
        }

        if (getTask.Result.Exists == true && int.TryParse(getTask.Result.Value.ToString(), out int savedValue)) // string으로 불러온 ingame currency를 tryparse로 savedValue에 저장
        {
            tempBluePrint = savedValue; // tempIngameCurrency = 기존 재화
        }

        int newBluePrint = tempBluePrint + bluePrintToAdd; // 재화 최신화

        var DBTask = dbRef.Child("users").Child(user.UserId).Child(user.DisplayName).Child("rewardBluePrint").SetValueAsync(newBluePrint); // 최신화 된 재화 DB 저장

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"reason : {DBTask.Exception}");
        }

        else
        {
            // 업데이트 된 재화 디스플레이
        }

        // BluePrint 캐싱
        Blueprint = newBluePrint;
    }

    #endregion

    #region Winning Rate

    public IEnumerator UpdateWinningRate() // 전적 업데이트 함수. PvP 스테이지 끝날 때 호출해야 함
    {
        float playCount = 0;

        float winCount = 0;

        float winningRate = 0;

        var getTask = dbRef.Child("users").Child(user.UserId).Child("rate").Child("playCount").GetValueAsync();

        yield return new WaitUntil(predicate: () => getTask.IsCompleted);

        if (getTask.Result.Exists == true)
        {
            playCount = float.Parse(getTask.Result.Value.ToString());

            PlayCount = playCount;
        }

        getTask = dbRef.Child("users").Child(user.UserId).Child("rate").Child("winCount").GetValueAsync();

        yield return new WaitUntil(predicate: () => getTask.IsCompleted);

        if (getTask.Result.Exists == true)
        {
            winCount = float.Parse(getTask.Result.Value.ToString());

            WinCount = winCount;
        }

        if (playCount > 0)
        {
            winningRate = (winCount / playCount) * 100;
        }
        else
        {
            winningRate = 0;
        }

        WinningRate = winningRate;

        var DBTask = dbRef.Child("users").Child(user.UserId).Child("rate").Child("winningRate").SetValueAsync(winningRate);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);
    }

    #endregion

    #region Tile management(예정)





    #endregion

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
