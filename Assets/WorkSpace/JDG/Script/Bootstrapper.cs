using JDG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JDG
{
    public class Bootstrapper : MonoBehaviour
    {
        private static bool _initialized = false;
        [SerializeField] private SceneLoader sceneLoaderPrefab;
        [SerializeField] private GameStateManager stateManager;

        private void Awake()
        {
            if (_initialized)
            {
                Destroy(gameObject);
                return;
            }

            _initialized = true;
            DontDestroyOnLoad(gameObject);

            if (SceneLoader.Instance == null)
            {
                Instantiate(sceneLoaderPrefab);
            }

            if (GameStateManager.Instance == null)
            {
                Instantiate(stateManager);
            }
        }
    }
}
