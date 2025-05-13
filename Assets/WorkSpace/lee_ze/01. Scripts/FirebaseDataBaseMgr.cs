using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using System.Threading.Tasks;
using TMPro;
using UnityEngine.UIElements;

public class FirebaseDataBaseMgr : MonoBehaviour
{
    DatabaseReference dbRef;

    FirebaseUser user;

    [SerializeField]
    private TMP_InputField rewardIngameCurrencyField;

    [SerializeField]
    private TMP_InputField rewardMetaCurrencyField;

    private void Start()
    {
        this.dbRef = FirebaseAuthMgr.dbRef;

        this.user = FirebaseAuthMgr.user;
    }

    public void SaveCurrencyInDataBase() // ingame, meta ��ȭ ����
    {
        StartCoroutine(UpdateRewardIngameCurrency(int.Parse(rewardIngameCurrencyField.text))); // reward�� ���ڰ����� �ָ� �ش� ���� ���ϰ� �ؾߵ�.

        StartCoroutine(UpdateRewardMetaCurrency(int.Parse(rewardMetaCurrencyField.text)));
    }

    private IEnumerator UpdateRewardIngameCurrency(int ingameCurrency)
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

        int newIngameCurrency = tempIngameCurrency + ingameCurrency; // ��ȭ �ֽ�ȭ

        var DBTask = dbRef.Child("users").Child(user.UserId).Child("rewardIngameCurrency").SetValueAsync(newIngameCurrency); // �ֽ�ȭ �� ��ȭ DB ����

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"reason : {DBTask.Exception}");
        }

        else
        {

        }
    }

    private IEnumerator UpdateRewardMetaCurrency(int metaCurrency)
    {
        int tempMetaCurrency = 0;

        var getTask = dbRef.Child("user").Child(user.UserId).Child("rewardMetaCurrency").GetValueAsync(); // ���� ��Ÿ ��ȭ �ҷ�����

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

        int newMetaCurrency = tempMetaCurrency + metaCurrency; // ��ȭ �ֽ�ȭ

        var DBTask = dbRef.Child("users").Child(user.UserId).Child("rewardMetaCurrency").SetValueAsync(metaCurrency);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"reason : {DBTask.Exception}");
        }

        else
        {

        }
    }
}
