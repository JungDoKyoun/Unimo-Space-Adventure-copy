using JDG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JDG
{
    public class Bootstrapper : MonoBehaviour
    {
        private static bool _initialized = false;
        [SerializeField] private SceneLoader _sceneLoaderPrefab;
        [SerializeField] private GameStateManager _stateManagerPrefab;
        [SerializeField] private RewardManager _rewardManagerPrefab;

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
                Instantiate(_sceneLoaderPrefab);
            }

            if (GameStateManager.Instance == null)
            {
                Instantiate(_stateManagerPrefab);
            }

            if (RewardManager.Instance == null)
            {
                Instantiate(_rewardManagerPrefab);
            }
        }
    }
}
