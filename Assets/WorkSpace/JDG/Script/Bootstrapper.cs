using JDG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    private static bool _initialized = false;
    [SerializeField] private SceneLoader sceneLoaderPrefab;
    [SerializeField] private GameStateManager stateManager;

    private void Awake()
    {
        if (_initialized)
        {
            Destroy(gameObject); // 중복 방지
            return;
        }

        _initialized = true;
        DontDestroyOnLoad(gameObject);

        if (SceneLoader.Instance == null)
        {
            Debug.Log("생성");
            Instantiate(sceneLoaderPrefab);
        }

        if (GameStateManager.Instance == null)
            Instantiate(stateManager);
    }
}
