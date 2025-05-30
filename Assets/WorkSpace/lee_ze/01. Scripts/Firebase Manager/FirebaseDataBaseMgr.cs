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

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) // �� �ٲ� �� ���� ����Ǵ� ��
    {
        if (user != null)
        {
            // ��ȭ ������Ʈ
            StartCoroutine(ShowUserIngameCurrency());

            StartCoroutine(ShowUserMetaCurrency());
        }
    }

    #region Currency management

    // >>>>>>>>> Ingame Currency <<<<<<<<<

    public IEnumerator InitIngameCurrency() // ���� Ŭ���� ���� �� �ΰ��� ��ȭ �ʱ�ȭ
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
            // ���⿡ ���÷���
            rewardIngameCurrencyText = GameObject.Find("Reward Ingame Currency")?.GetComponent<TextMeshProUGUI>();

            if(rewardIngameCurrencyText != null) rewardIngameCurrencyText.text = savedValue.ToString();
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

        var getTask = dbRef.Child("users").Child(user.UserId).Child(user.DisplayName).Child("rewardIngameCurrency").GetValueAsync(); // ���� �ΰ��� ��ȭ �ҷ�����

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

        var DBTask = dbRef.Child("users").Child(user.UserId).Child(user.DisplayName).Child("rewardIngameCurrency").SetValueAsync(newIngameCurrency); // �ֽ�ȭ �� ��ȭ DB ����

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
    }

    // >>>>>>>>> Meta Currency <<<<<<<<<

    private IEnumerator ShowUserMetaCurrency()
    {
        var getTask = dbRef.Child("users").Child(user.UserId).Child(user.DisplayName).Child("rewardMetaCurrency").GetValueAsync();

        yield return new WaitUntil(predicate: () => getTask.IsCompleted);

        if (getTask.Result.Exists == true && int.TryParse(getTask.Result.Value.ToString(), out int savedValue))
        {
            // ���⿡ ���÷���
            rewardMetaCurrencyText = GameObject.Find("Reward Meta Currency")?.GetComponent<TextMeshProUGUI>();

            if (rewardIngameCurrencyText != null) rewardMetaCurrencyText.text = savedValue.ToString();
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

        var getTask = dbRef.Child("users").Child(user.UserId).Child(user.DisplayName).Child("rewardMetaCurrency").GetValueAsync(); // ���� ��Ÿ ��ȭ �ҷ�����

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

        var DBTask = dbRef.Child("users").Child(user.UserId).Child(user.DisplayName).Child("rewardMetaCurrency").SetValueAsync(newMetaCurrency);

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

    /// <summary>
    /// ���ڰ�: ��ȭ �߰� �� >> ��� | ��ȭ ��� �� >> ����
    /// </summary>
    /// <param name="bluePrintToAdd"></param>
    /// <returns></returns>
    public IEnumerator UpdateRewardBluePrint(int bluePrintToAdd) // Blue Print ��ȭ ���� �Լ�(���� ��)
    {
        int tempBluePrint = 0;

        var getTask = dbRef.Child("users").Child(user.UserId).Child(user.DisplayName).Child("rewardBluePrint").GetValueAsync(); // ���� ���赵 �ҷ�����

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

        var DBTask = dbRef.Child("users").Child(user.UserId).Child(user.DisplayName).Child("rewardBluePrint").SetValueAsync(newBluePrint); // �ֽ�ȭ �� ��ȭ DB ����

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"reason : {DBTask.Exception}");
        }

        else
        {
            // ������Ʈ �� ��ȭ ���÷���
        }

        // BluePrint ĳ��
        Blueprint = newBluePrint;
    }

    #endregion

    #region Winning Rate

    public IEnumerator UpdateWinningRate() // ���� ������Ʈ �Լ�. PvP �������� ���� �� ȣ���ؾ� ��
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

    #region Tile management(����)





    #endregion

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
