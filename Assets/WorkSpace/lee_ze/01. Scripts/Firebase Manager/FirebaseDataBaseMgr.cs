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

    public static IReadOnlyList<(string nickname, float score)> TopRankers => topRankers.AsReadOnly(); // ����Ʈ ������Ƽ

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
        // Firebase ���� ���
        yield return new WaitUntil(() => FirebaseAuthMgr.IsFirebaseReady == true);

        // �α��� ���
        yield return new WaitUntil(() => FirebaseAuthMgr.User != null);

        this.dbRef = FirebaseAuthMgr.DBRef;

        this.user = FirebaseAuthMgr.User;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) // �� �ٲ� �� ���� ����Ǵ� ��
    {
        if (user != null)
        {
            // ��ȭ ������Ʈ
            StartCoroutine(ShowUserIngameCurrency());

            StartCoroutine(ShowUserMetaCurrency());

            StartCoroutine(ShowUserBluePrint());

            // ��ũ ������Ʈ
            StartCoroutine(UpdateRankingList());
        }
    }

    #region Currency management

    // >>>>>>>>> Ingame Currency <<<<<<<<<

    public IEnumerator InitIngameCurrency() // ���� Ŭ���� ���� �� �ΰ��� ��ȭ �ʱ�ȭ
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
            // ���⿡ ���÷���
            rewardIngameCurrencyText = GameObject.Find("Database Get Canvas")?.transform.Find("Reward Ingame Currency").GetComponent<TextMeshProUGUI>();

            if (rewardIngameCurrencyText != null) rewardIngameCurrencyText.text = savedValue.ToString();

            IngameCurrency = savedValue;
        }
    }

    /// <summary>
    /// ���ڰ�: ��ȭ �߰� �� >> ��� | ��ȭ ��� �� >> ����
    /// </summary>
    /// <param name="ingameCurrencyToAdd"></param>
    /// <returns></returns>
    public IEnumerator UpdateRewardIngameCurrency(int ingameCurrencyToAdd) // Ingame ��ȭ ���� �Լ�(���� ��)
    {
        int tempIngameCurrency = 0;

        var getTask = dbRef.Child("users").Child(user.UserId).Child("rewardIngameCurrency").GetValueAsync(); // ���� �ΰ��� ��ȭ �ҷ�����

        yield return new WaitUntil(predicate: () => getTask.IsCompleted);

        if (getTask.Exception != null) // �ҷ����� ���� �� ������
        {
            Debug.LogWarning($"[Get] reason : {getTask.Exception}");

            yield break;
        }

        if (getTask.Result.Exists == true && int.TryParse(getTask.Result.Value.ToString(), out int savedValue)) // string���� �ҷ��� ingame currency�� tryparse�� savedValue�� ����
        {
            tempIngameCurrency = savedValue; // tempIngameCurrency = ���� ��ȭ
        }

        int newIngameCurrency = tempIngameCurrency + ingameCurrencyToAdd; // ��ȭ �ֽ�ȭ

        var DBTask = dbRef.Child("users").Child(user.UserId).Child("rewardIngameCurrency").SetValueAsync(newIngameCurrency); // �ֽ�ȭ �� ��ȭ DB ����

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"reason : {DBTask.Exception}");
        }

        else
        {
            // ������Ʈ �� ��ȭ ���÷���
            StartCoroutine(ShowUserIngameCurrency());
        }

        // IngameCurrency ĳ��
        IngameCurrency = newIngameCurrency;
    }

    // >>>>>>>>> Meta Currency <<<<<<<<<

    private IEnumerator ShowUserMetaCurrency()
    {
        var getTask = dbRef.Child("users").Child(user.UserId).Child("rewardMetaCurrency").GetValueAsync();

        yield return new WaitUntil(predicate: () => getTask.IsCompleted);

        if (getTask.Result.Exists == true && int.TryParse(getTask.Result.Value.ToString(), out int savedValue))
        {
            // ���⿡ ���÷���
            rewardMetaCurrencyText = GameObject.Find("Database Get Canvas")?.transform.Find("Reward Meta Currency").GetComponent<TextMeshProUGUI>();

            if (rewardMetaCurrencyText != null) rewardMetaCurrencyText.text = savedValue.ToString();

            MetaCurrency = savedValue;
        }
    }

    /// <summary>
    /// ���ڰ�: ��ȭ �߰� �� >> ��� | ��ȭ ��� �� >> ����
    /// </summary>
    /// <param name="metaCurrencyToAdd"></param>
    /// <returns></returns>
    public IEnumerator UpdateRewardMetaCurrency(int metaCurrencyToAdd) // Meta ��ȭ ���� �Լ�(���� ��)
    {
        int tempMetaCurrency = 0;

        var getTask = dbRef.Child("users").Child(user.UserId).Child("rewardMetaCurrency").GetValueAsync(); // ���� ��Ÿ ��ȭ �ҷ�����

        yield return new WaitUntil(predicate: () => getTask.IsCompleted);

        if (getTask.Exception != null)
        {
            Debug.LogWarning($"[Get] reason : {getTask.Exception}");

            yield break;
        }

        if (getTask.Result.Exists == true && int.TryParse(getTask.Result.Value.ToString(), out int savedValue)) // string���� �ҷ��� meta currency�� tryparse�� savedValue�� ����
        {
            tempMetaCurrency = savedValue; // tempMetaCurrency = ���� ��ȭ
        }

        int newMetaCurrency = tempMetaCurrency + metaCurrencyToAdd; // ��ȭ �ֽ�ȭ

        var DBTask = dbRef.Child("users").Child(user.UserId).Child("rewardMetaCurrency").SetValueAsync(newMetaCurrency);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"reason : {DBTask.Exception}");
        }
        else
        {
            // ������Ʈ �� ��ȭ ���÷���
            StartCoroutine(ShowUserMetaCurrency());
        }

        // MetaCurrency ĳ��
        MetaCurrency = newMetaCurrency;
    }

    // >>>>>>>>> BluePrint <<<<<<<<<

    private IEnumerator ShowUserBluePrint()
    {
        var getTask = dbRef.Child("users").Child(user.UserId).Child("rewardBluePrint").GetValueAsync();

        yield return new WaitUntil(predicate: () => getTask.IsCompleted);

        if (getTask.Result.Exists == true && int.TryParse(getTask.Result.Value.ToString(), out int savedValue))
        {
            // ���⿡ ���÷���
            rewardBluePrintText = GameObject.Find("Database Get Canvas")?.transform.Find("Reward Blue Print").GetComponent<TextMeshProUGUI>();

            if (rewardBluePrintText != null) rewardBluePrintText.text = savedValue.ToString();

            Blueprint = savedValue;
        }
    }

    /// <summary>
    /// ���ڰ�: ��ȭ �߰� �� >> ��� | ��ȭ ��� �� >> ����
    /// </summary>
    /// <param name="bluePrintToAdd"></param>
    /// <returns></returns>
    public IEnumerator UpdateRewardBluePrint(int bluePrintToAdd) // Blue Print ��ȭ ���� �Լ�(���� ��)
    {
        int tempBluePrint = 0;

        var getTask = dbRef.Child("users").Child(user.UserId).Child("rewardBluePrint").GetValueAsync(); // ���� ���赵 �ҷ�����

        yield return new WaitUntil(predicate: () => getTask.IsCompleted);

        if (getTask.Exception != null) // �ҷ����� ���� �� ������
        {
            Debug.LogWarning($"[Get] reason : {getTask.Exception}");

            yield break;
        }

        if (getTask.Result.Exists == true && int.TryParse(getTask.Result.Value.ToString(), out int savedValue)) // string���� �ҷ��� ingame currency�� tryparse�� savedValue�� ����
        {
            tempBluePrint = savedValue; // tempIngameCurrency = ���� ��ȭ
        }

        int newBluePrint = tempBluePrint + bluePrintToAdd; // ��ȭ �ֽ�ȭ

        var DBTask = dbRef.Child("users").Child(user.UserId).Child("rewardBluePrint").SetValueAsync(newBluePrint); // �ֽ�ȭ �� ��ȭ DB ����

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"reason : {DBTask.Exception}");
        }

        else
        {
            // ������Ʈ �� ��ȭ ���÷���
            StartCoroutine(ShowUserBluePrint());
        }

        // BluePrint ĳ��
        Blueprint = newBluePrint;
    }

    #endregion

    #region Winning Rate (�̻��)

    public IEnumerator SetCurrentWinningRate() // ���� ������Ʈ �Լ�.
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

    // PvP ���� ��� ���� �� ȣ��Ǿ�� �� �Լ�
    public IEnumerator UpdateWinningRate(bool winner)
    {
        // �÷��̾� ���� PlayCount ����
        PlayCount++;

        var DBTask = dbRef.Child("users").Child(user.UserId).Child("rate").Child("playCount").SetValueAsync(PlayCount);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (winner == true)
        {
            // �̱� �÷��̾ WinCount ����
            WinCount++;

            DBTask = dbRef.Child("users").Child(user.UserId).Child("rate").Child("winCount").SetValueAsync(WinCount);
        }

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);
    }

    #endregion

    #region Score management

    /// <summary>
    /// ���� ������Ʈ �� �� ���.
    /// </summary>
    /// <param name="currentScore"></param>
    /// <returns></returns>
    public IEnumerator UpdateScore(float currentScore)
    {
        CurrentScore = currentScore;

        var getTask = dbRef.Child("users").Child(user.UserId).Child("score").GetValueAsync();

        yield return new WaitUntil(predicate: () => getTask.IsCompleted);

        if (getTask.Exception != null) // �ҷ����� ���� �� ������
        {
            Debug.LogWarning($"Get error reason : {getTask.Exception}");

            yield break;
        }

        if (getTask.Result.Exists == true && float.TryParse(getTask.Result.Value.ToString(), out float savedScore)) // string���� �ҷ��� ingame currency�� tryparse�� savedValue�� ����
        {
            if (CurrentScore > savedScore) // ���� �ְ� ���(savedScore)���� ��� ���� ���(currentScore)�� ũ��
            {
                // �ְ��� ����
                var DBTask = dbRef.Child("user").Child(user.UserId).Child("score").SetValueAsync(CurrentScore);

                yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

                // ��ü ��ŷ ������Ʈ
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

        topRankers.Clear(); // ���� ��ŷ �ʱ�ȭ

        DataSnapshot snapshot = getTask.Result;

        foreach (var userSnapshot in snapshot.Children)
        {
            string nickname = "";

            float score = 0f;

            // Score ��������
            if (userSnapshot.HasChild("score") && float.TryParse(userSnapshot.Child("score").Value.ToString(), out float parsedScore))
            {
                score = parsedScore;
            }

            // Nickname ��������
            if (userSnapshot.HasChild("nickname") == true)
            {
                nickname = userSnapshot.Child("nickname").Value.ToString();
            }

            // ����Ʈ �߰�
            topRankers.Add((nickname, score));
        }

        // �������� ����
        topRankers.Sort((a, b) => b.score.CompareTo(a.score));

        // ���� �������� ǥ�� 
        foreach (var (nickname, score) in topRankers)
        {
            Debug.Log($"Nickname: {nickname}, Score: {score}");
        }
    }

    #endregion

    #region Tile management(�ļ���)





    #endregion

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
