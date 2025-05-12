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

    public void SaveCurrencyInDataBase() // ingame, meta 재화 저장
    {
        StartCoroutine(UpdateRewardIngameCurrency(int.Parse(rewardIngameCurrencyField.text))); // reward를 인자값으로 주면 해당 값을 더하게 해야됨.

        StartCoroutine(UpdateRewardMetaCurrency(int.Parse(rewardMetaCurrencyField.text)));
    }

    private IEnumerator UpdateRewardIngameCurrency(int ingameCurrency)
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

        int newIngameCurrency = tempIngameCurrency + ingameCurrency; // 재화 최신화

        var DBTask = dbRef.Child("users").Child(user.UserId).Child("rewardIngameCurrency").SetValueAsync(newIngameCurrency); // 최신화 된 재화 DB 저장

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

        var getTask = dbRef.Child("user").Child(user.UserId).Child("rewardMetaCurrency").GetValueAsync(); // 현재 메타 재화 불러오기

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

        int newMetaCurrency = tempMetaCurrency + metaCurrency; // 재화 최신화

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
