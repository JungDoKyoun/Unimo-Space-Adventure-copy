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

    [SerializeField]
    private TextMeshProUGUI rewardBluePrintText;



    private static List<(string nickname, float score)> topRankers = new List<(string, float)>();

    private static int ingameCurrency;

    private static int metaCurrency;

    private static int bluePrint;

    private static float winningRate;

    private static float playCount;

    private static float winCount;

    private static float currentScore;

    #region properties

    public static IReadOnlyList<(string nickname, float score)> TopRankers => topRankers.AsReadOnly(); // 리스트 프로퍼티

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

    public static float CurrentScore
    {
        get => currentScore;

        set
        {
            currentScore = value;
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
        // Firebase 연결 대기
        yield return new WaitUntil(() => FirebaseAuthMgr.IsFirebaseReady == true);

        // 로그인 대기
        yield return new WaitUntil(() => FirebaseAuthMgr.User != null);

        this.dbRef = FirebaseAuthMgr.DBRef;

        this.user = FirebaseAuthMgr.User;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) // 씬 바뀔 때 마다 수행되는 것
    {
        if (user != null)
        {
            // 재화 업데이트
            StartCoroutine(ShowUserIngameCurrency());

            StartCoroutine(ShowUserMetaCurrency());

            StartCoroutine(ShowUserBluePrint());

            // 랭크 업데이트
            StartCoroutine(UpdateRankingList());
        }
    }

    #region Currency management

    // >>>>>>>>> Ingame Currency <<<<<<<<<

    public IEnumerator InitIngameCurrency() // 게임 클리어 실패 시 인게임 재화 초기화
    {
        var getTask = dbRef.Child("users").Child(user.UserId).Child("rewardIngameCurrency").SetValueAsync(0);

        yield return new WaitUntil(predicate: () => getTask.IsCompleted);
    }

    private IEnumerator ShowUserIngameCurrency()
    {
        var getTask = dbRef.Child("users").Child(user.UserId).Child("rewardIngameCurrency").GetValueAsync();

        yield return new WaitUntil(predicate: () => getTask.IsCompleted);

        if (getTask.Result.Exists == true && int.TryParse(getTask.Result.Value.ToString(), out int savedValue))
        {
            // 여기에 디스플레이
            rewardIngameCurrencyText = GameObject.Find("Database Get Canvas")?.transform.Find("Reward Ingame Currency").GetComponent<TextMeshProUGUI>();

            if (rewardIngameCurrencyText != null) rewardIngameCurrencyText.text = savedValue.ToString();

            IngameCurrency = savedValue;
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

        var getTask = dbRef.Child("users").Child(user.UserId).Child("rewardIngameCurrency").GetValueAsync(); // 현재 인게임 재화 불러오기

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

        var DBTask = dbRef.Child("users").Child(user.UserId).Child("rewardIngameCurrency").SetValueAsync(newIngameCurrency); // 최신화 된 재화 DB 저장

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

        // IngameCurrency 캐싱
        IngameCurrency = newIngameCurrency;
    }

    // >>>>>>>>> Meta Currency <<<<<<<<<

    private IEnumerator ShowUserMetaCurrency()
    {
        var getTask = dbRef.Child("users").Child(user.UserId).Child("rewardMetaCurrency").GetValueAsync();

        yield return new WaitUntil(predicate: () => getTask.IsCompleted);

        if (getTask.Result.Exists == true && int.TryParse(getTask.Result.Value.ToString(), out int savedValue))
        {
            // 여기에 디스플레이
            rewardMetaCurrencyText = GameObject.Find("Database Get Canvas")?.transform.Find("Reward Meta Currency").GetComponent<TextMeshProUGUI>();

            if (rewardMetaCurrencyText != null) rewardMetaCurrencyText.text = savedValue.ToString();

            MetaCurrency = savedValue;
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

        var getTask = dbRef.Child("users").Child(user.UserId).Child("rewardMetaCurrency").GetValueAsync(); // 현재 메타 재화 불러오기

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

        var DBTask = dbRef.Child("users").Child(user.UserId).Child("rewardMetaCurrency").SetValueAsync(newMetaCurrency);

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

    private IEnumerator ShowUserBluePrint()
    {
        var getTask = dbRef.Child("users").Child(user.UserId).Child("rewardBluePrint").GetValueAsync();

        yield return new WaitUntil(predicate: () => getTask.IsCompleted);

        if (getTask.Result.Exists == true && int.TryParse(getTask.Result.Value.ToString(), out int savedValue))
        {
            // 여기에 디스플레이
            rewardBluePrintText = GameObject.Find("Database Get Canvas")?.transform.Find("Reward Blue Print").GetComponent<TextMeshProUGUI>();

            if (rewardBluePrintText != null) rewardBluePrintText.text = savedValue.ToString();

            Blueprint = savedValue;
        }
    }

    /// <summary>
    /// 인자값: 재화 추가 시 >> 양수 | 재화 사용 시 >> 음수
    /// </summary>
    /// <param name="bluePrintToAdd"></param>
    /// <returns></returns>
    public IEnumerator UpdateRewardBluePrint(int bluePrintToAdd) // Blue Print 재화 저장 함수(더할 값)
    {
        int tempBluePrint = 0;

        var getTask = dbRef.Child("users").Child(user.UserId).Child("rewardBluePrint").GetValueAsync(); // 현재 설계도 불러오기

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

        var DBTask = dbRef.Child("users").Child(user.UserId).Child("rewardBluePrint").SetValueAsync(newBluePrint); // 최신화 된 재화 DB 저장

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"reason : {DBTask.Exception}");
        }

        else
        {
            // 업데이트 된 재화 디스플레이
            StartCoroutine(ShowUserBluePrint());
        }

        // BluePrint 캐싱
        Blueprint = newBluePrint;
    }

    #endregion

    #region Winning Rate (미사용)

    public IEnumerator SetCurrentWinningRate() // 전적 업데이트 함수.
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

    // PvP 승자 결과 나올 때 호출되어야 할 함수
    public IEnumerator UpdateWinningRate(bool winner)
    {
        // 플레이어 전부 PlayCount 증가
        PlayCount++;

        var DBTask = dbRef.Child("users").Child(user.UserId).Child("rate").Child("playCount").SetValueAsync(PlayCount);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (winner == true)
        {
            // 이긴 플레이어만 WinCount 증가
            WinCount++;

            DBTask = dbRef.Child("users").Child(user.UserId).Child("rate").Child("winCount").SetValueAsync(WinCount);
        }

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);
    }

    #endregion

    #region Score management

    /// <summary>
    /// 점수 업데이트 할 때 사용.
    /// </summary>
    /// <param name="currentScore"></param>
    /// <returns></returns>
    public IEnumerator UpdateScore(float currentScore)
    {
        CurrentScore = currentScore;

        var getTask = dbRef.Child("users").Child(user.UserId).Child("score").GetValueAsync();

        yield return new WaitUntil(predicate: () => getTask.IsCompleted);

        if (getTask.Exception != null) // 불러오기 실패 시 나가기
        {
            Debug.LogWarning($"Get error reason : {getTask.Exception}");

            yield break;
        }

        if (getTask.Result.Exists == true && float.TryParse(getTask.Result.Value.ToString(), out float savedScore)) // string으로 불러온 ingame currency를 tryparse로 savedValue에 저장
        {
            if (CurrentScore > savedScore) // 기존 최고 기록(savedScore)보다 방금 세운 기록(currentScore)이 크면
            {
                // 최고기록 갱신
                var DBTask = dbRef.Child("user").Child(user.UserId).Child("score").SetValueAsync(CurrentScore);

                yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

                // 전체 랭킹 업데이트
                StartCoroutine(UpdateRankingList());
            }
        }
    }

    private IEnumerator UpdateRankingList()
    {
        var getTask = dbRef.Child("users").OrderByChild("score").LimitToLast(10).GetValueAsync();

        yield return new WaitUntil(() => getTask.IsCompleted);

        if (getTask.Exception != null)
        {
            Debug.LogError($"Ranking Error: {getTask.Exception}");

            yield break;
        }

        topRankers.Clear(); // 기존 랭킹 초기화

        DataSnapshot snapshot = getTask.Result;

        foreach (var userSnapshot in snapshot.Children)
        {
            string nickname = "";

            float score = 0f;

            // Score 가져오기
            if (userSnapshot.HasChild("score") && float.TryParse(userSnapshot.Child("score").Value.ToString(), out float parsedScore))
            {
                score = parsedScore;
            }

            // Nickname 가져오기
            if (userSnapshot.HasChild("nickname") == true)
            {
                nickname = userSnapshot.Child("nickname").Value.ToString();
            }

            // 리스트 추가
            topRankers.Add((nickname, score));
        }

        // 내림차순 정렬
        topRankers.Sort((a, b) => b.score.CompareTo(a.score));

        // 순위 내림차순 표시 
        foreach (var (nickname, score) in topRankers)
        {
            Debug.Log($"Nickname: {nickname}, Score: {score}");
        }
    }

    #endregion

    #region Tile management(후순위)





    #endregion

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
