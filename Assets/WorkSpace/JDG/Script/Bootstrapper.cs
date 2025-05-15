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
            Destroy(gameObject); // �ߺ� ����
            return;
        }

        _initialized = true;
        DontDestroyOnLoad(gameObject);

        if (SceneLoader.Instance == null)
        {
            Debug.Log("����");
            Instantiate(sceneLoaderPrefab);
        }

        if (GameStateManager.Instance == null)
            Instantiate(stateManager);
    }
}
