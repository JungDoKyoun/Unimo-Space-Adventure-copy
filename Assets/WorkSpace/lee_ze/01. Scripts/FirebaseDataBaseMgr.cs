using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using TMPro;

public class FirebaseDataBaseMgr : MonoBehaviour
{
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

    private IEnumerator Start()
    {
        yield return new WaitUntil(() => FirebaseAuthMgr.IsFirebaseReady == true);

        Debug.Log("user �����");

        yield return new WaitUntil(() => FirebaseAuthMgr.user != null);

        Debug.Log(FirebaseAuthMgr.IsFirebaseReady);

        this.dbRef = FirebaseAuthMgr.dbRef;

        this.user = FirebaseAuthMgr.user;

        StartCoroutine(ShowUserIngameCurrency());

        StartCoroutine(ShowUserMetaCurrency());
    }

    #region Currency management

    private IEnumerator ShowUserIngameCurrency()
    {
        var getTask = dbRef.Child("users").Child(user.UserId).Child("rewardIngameCurrency").GetValueAsync();

        yield return new WaitUntil(predicate: () => getTask.IsCompleted);

        if (getTask.Result.Exists == true && int.TryParse(getTask.Result.Value.ToString(), out int savedValue))
        {
            rewardIngameCurrencyText.text = "InGame Currency: " + savedValue.ToString();
        }
    }

    private IEnumerator ShowUserMetaCurrency()
    {
        var getTask = dbRef.Child("users").Child(user.UserId).Child("rewardMetaCurrency").GetValueAsync();

        yield return new WaitUntil(predicate: () => getTask.IsCompleted);

        if (getTask.Result.Exists == true && int.TryParse(getTask.Result.Value.ToString(), out int savedValue))
        {
            rewardMetaCurrencyText.text = "Meta Currency: " + savedValue.ToString();
        }
    }

    public void SaveCurrencyInDataBase() // ��ü ��ȭ ���� >> ��ư �̺�Ʈ �Լ��� ȣ��
    {
        StartCoroutine(UpdateRewardIngameCurrency(int.Parse(rewardIngameCurrencyField.text))); // reward�� ���ڰ����� �ָ� �ش� ���� ���ϰ� �ؾߵ�.

        StartCoroutine(UpdateRewardMetaCurrency(int.Parse(rewardMetaCurrencyField.text)));
    }

    private IEnumerator UpdateRewardIngameCurrency(int ingameCurrencyToAdd) // Ingame ��ȭ ���� �Լ�(���� ��)
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
            // ��ȭ ���÷���
            StartCoroutine(ShowUserIngameCurrency());
        }
    }

    private IEnumerator UpdateRewardMetaCurrency(int metaCurrencyToAdd) // Meta ��ȭ ���� �Լ�(���� ��)
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
            // ��ȭ ���÷���
            StartCoroutine(ShowUserMetaCurrency());
        }
    }

    #endregion

    #region Tile management




    #endregion
}
