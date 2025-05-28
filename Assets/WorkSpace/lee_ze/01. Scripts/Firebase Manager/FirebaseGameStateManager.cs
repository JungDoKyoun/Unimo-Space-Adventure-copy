using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using Firebase.Auth;
using JDG;

public class FirebaseGameStateManager : MonoBehaviour
{
    private DatabaseReference dbRef;

    private FirebaseUser user;

    private void Start()
    {
        StartCoroutine(InitFirebase());
    }

    private IEnumerator InitFirebase()
    {
        yield return new WaitUntil(() => FirebaseAuthMgr.IsFirebaseReady);

        yield return new WaitUntil(() => FirebaseAuthMgr.User != null);

        dbRef = FirebaseAuthMgr.dbRef;

        user = FirebaseAuthMgr.User;
    }

    public void SaveGameStateToFirebase(Dictionary<Vector2Int, TileData> tileDataDict, Vector2Int playerCoord)
    {
        Dictionary<string, object> saveData = new Dictionary<string, object>();

        foreach (var pair in tileDataDict)
        {
            string key = pair.Key.x + "," + pair.Key.y;

            saveData[key] = pair.Value.ToDictionary();
        }

        dbRef.Child("users").Child(user.UserId).Child("tileSaveData").SetValueAsync(saveData);

        dbRef.Child("users").Child(user.UserId).Child("playerCoord").Child("x").SetValueAsync(playerCoord.x);

        dbRef.Child("users").Child(user.UserId).Child("playerCoord").Child("y").SetValueAsync(playerCoord.y);
    }

    public void LoadGameStateFromFirebase(Action<Dictionary<Vector2Int, TileData>, Vector2Int> onLoaded)
    {
        StartCoroutine(LoadGameStateCoroutine(onLoaded));
    }

    private IEnumerator LoadGameStateCoroutine(Action<Dictionary<Vector2Int, TileData>, Vector2Int> onLoaded)
    {
        var tileTask = dbRef.Child("users").Child(user.UserId).Child("tileSaveData").GetValueAsync();

        var coordTask = dbRef.Child("users").Child(user.UserId).Child("playerCoord").GetValueAsync();

        yield return new WaitUntil(() => tileTask.IsCompleted && coordTask.IsCompleted);

        Dictionary<Vector2Int, TileData> tileDataDict = new Dictionary<Vector2Int, TileData>();

        if (tileTask.Result.Exists)
        {
            foreach (var child in tileTask.Result.Children)
            {
                string[] coordStr = child.Key.Split(',');

                int x = int.Parse(coordStr[0]);

                int y = int.Parse(coordStr[1]);

                Dictionary<string, object> rawData = child.Value as Dictionary<string, object>;

                TileData tileData = TileData.FromDictionary(rawData);

                tileDataDict[new Vector2Int(x, y)] = tileData;
            }
        }

        Vector2Int playerCoord = Vector2Int.zero;
        if (coordTask.Result.Exists)
        {
            int x = Convert.ToInt32(coordTask.Result.Child("x").Value);

            int y = Convert.ToInt32(coordTask.Result.Child("y").Value);

            playerCoord = new Vector2Int(x, y);
        }

        onLoaded?.Invoke(tileDataDict, playerCoord);
    }
}
