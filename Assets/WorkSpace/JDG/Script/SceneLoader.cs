using System.Collections;
using System.Collections.Generic;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JDG
{
    public class SceneLoader : MonoBehaviour
    {
        private static SceneLoader _instance;
        private string _wordMapScene = "WorldMapScene";
        private string _currentScene;
        private HexRenderer _choseTile;
        private HexGridLayout _hexGridLayout;
        private PlayerController _playerController;

        //private void Awake()
        //{
        //    if (_instance != null && _instance != this)
        //    {
        //        Debug.LogWarning("¾À·Î´õ Áö¿öÁü");
        //        Destroy(gameObject);
        //        return;
        //    }

        //    Debug.LogWarning("¾À·Î´õ ¸¸µé¾îÁü");
        //    _instance = this;
        //    DontDestroyOnLoad(gameObject);
        //    SceneManager.sceneLoaded += OnSceneLoaded;
        //}
        private void Awake()
        {
            if (_instance == null)
            {
                Debug.Log("»ý¼º");
                _instance = this;
                DontDestroyOnLoad(gameObject);
                SceneManager.sceneLoaded += OnSceneLoaded;
            }
            else
            {
                Debug.Log("ÆÄ±«");
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        public static SceneLoader Instance
        {
            get
            {
                return _instance;
            }
        }

        public void Init(HexGridLayout hexGrid, PlayerController playerController)
        {
            _hexGridLayout = hexGrid;
            _playerController = playerController;
        }

        public void EnterTileScene(HexRenderer tile)
        {
            Debug.Log(tile);
            _choseTile = tile;
            Debug.Log(_choseTile);
            GameStateManager.Instance.SaveTileStates(_hexGridLayout.HexMap, _hexGridLayout.PlayerCoord);

            _currentScene = tile.TileData.SceneName;
            SceneManager.LoadScene(_currentScene);
        }

        public void ReturnToWorldMap()
        {
            _currentScene = _wordMapScene;
            SceneManager.LoadScene(_currentScene);
        }

        public HexRenderer GetChoseTile()
        {
            return _choseTile;
        }

        public void ClearTile()
        {
            Debug.Log(_choseTile);
            if(_choseTile != null)
            {
                _choseTile.TileData.IsCleared = true;
                Debug.Log("Å¸ÀÏ Å¬¸®¾î");
            }
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if(scene.name == _wordMapScene)
            {
                var stateManager = GameStateManager.Instance;
                if(stateManager.IsRestoreMap)
                {
                    if (stateManager.TileSaveData != null && stateManager.TileSaveData.Count > 0)
                    {
                        HexGridLayout layout = FindObjectOfType<HexGridLayout>();
                        layout.RestoreMapState(stateManager.TileSaveData, stateManager.PlayerCoord);
                        stateManager.ResetIsRestoreMap();

                        StartCoroutine(MoveToClearedTile());
                    }
                }
            }
        }

        private IEnumerator MoveToClearedTile()
        {
            Debug.Log("µé¾î¿È");
            yield return null;

            if (_choseTile != null && _choseTile.TileData.IsCleared)
            {
                Debug.Log("µé¾î¿È2");
                Vector3 targetPos = _hexGridLayout.GetPositionForHexFromCoordinate(_choseTile.TileData.Coord) + Vector3.up;
                _playerController.MoveTo(targetPos);
            }
        }
    }
}
