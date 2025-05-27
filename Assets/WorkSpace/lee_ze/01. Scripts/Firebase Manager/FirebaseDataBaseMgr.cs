using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using TMPro;
using UnityEngine.SceneManagement;
using System;

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

    private static float rate;

    public static float Rate
    {
        get => rate;

        private set
        {
            rate = value;
        }
    }

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

        Debug.Log("user �����");

        yield return new WaitUntil(() => FirebaseAuthMgr.User != null);

        this.dbRef = FirebaseAuthMgr.dbRef;

        this.user = FirebaseAuthMgr.User;

        StartCoroutine(ShowUserIngameCurrency());

        StartCoroutine(ShowUserMetaCurrency());
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (user != null)
        {
            StartCoroutine(ShowUserIngameCurrency());

            StartCoroutine(ShowUserMetaCurrency());
        }
    }

    #region Currency management

    // >>>>>>>>> Ingame Currency

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

    public void SaveCurrencyInDataBase() // ��ü ��ȭ ���� >> ��ư �̺�Ʈ �Լ��� ȣ��
    {
        if (rewardIngameCurrencyField != null) StartCoroutine(UpdateRewardIngameCurrency(int.Parse(rewardIngameCurrencyField.text))); // reward�� ���ڰ����� �ָ� �ش� ���� ���ϰ� �ؾߵ�.
        else Debug.Log("Ingame empty");

        if (rewardMetaCurrencyField != null) StartCoroutine(UpdateRewardMetaCurrency(int.Parse(rewardMetaCurrencyField.text)));
        else Debug.Log("Meta empty");
    }

    // ���� Ŭ���� ���� �� �ΰ��� ��ȭ �ʱ�ȭ
    public IEnumerator InitIngameCurrency()
    {
        var getTask = dbRef.Child("users").Child(user.UserId).Child(user.DisplayName).Child("rewardIngameCurrency").SetValueAsync(0);

        yield return new WaitUntil(predicate: () => getTask.IsCompleted);
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


    // >>>>>>>>> Meta Currency

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
    }

    #endregion

    #region Winning Rate
    
    public IEnumerator SetWinningRate()
    {
        float playCount = 0;

        float winCount = 0;

        float winningRate = 0;

        var getTask = dbRef.Child("users").Child(user.UserId).Child("rate").Child("playCount").GetValueAsync();

        yield return new WaitUntil(predicate: () => getTask.IsCompleted);

        if (getTask.Result.Exists == true)
        {
            playCount = float.Parse(getTask.Result.Value.ToString());
        }

        getTask = dbRef.Child("users").Child(user.UserId).Child("rate").Child("winCount").GetValueAsync();

        yield return new WaitUntil(predicate: () => getTask.IsCompleted);

        if (getTask.Result.Exists == true)
        {
            winCount = float.Parse(getTask.Result.Value.ToString());
        }

        if (playCount > 0)
        {
            winningRate = (winCount / playCount) * 100;
        }
        else
        {
            winningRate = 0;
        }

        Rate = winningRate;

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
