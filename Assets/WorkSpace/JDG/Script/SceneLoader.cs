    using System.Collections;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using JDG;

    namespace JDG
    {
        public class SceneLoader : MonoBehaviour
        {
            private static SceneLoader _instance;
            private string _wordMapScene = "World Map Scene";
            private string _currentScene;
            private TileData _choseTileData;
            private HexGridLayout _hexGridLayout;
            private PlayerController _playerController;

            private void Awake()
            {
                if (_instance == null)
                {
                    _instance = this;
                    DontDestroyOnLoad(gameObject);
                }
                else
                {
                    Destroy(gameObject);
                }
            }

            public TileData ChoseTileData { get { return _choseTileData; } }

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
                _choseTileData = tile.TileData;
                GameStateManager.Instance.SaveTileStates(_hexGridLayout.HexMap, _hexGridLayout.PlayerCoord);

                _currentScene = tile.TileData.SceneName;
                SceneManager.LoadScene(_currentScene);
            }

            //public void ReturnToWorldMap()
            //{
            //    _currentScene = _wordMapScene;
            //    SceneManager.LoadScene(_currentScene);
            //}

            public TileData GetChoseTile()
            {
                return _choseTileData;
            }

            public void ClearTile()
            {
                if(_choseTileData != null)
                {
                    _choseTileData.IsCleared = true;
                    GameStateManager.Instance.UpdateTileState(_choseTileData);
                }
            }

            public void ReturnToWorldMap()
            {
                var stateManager = GameStateManager.Instance;
                if (GameStateManager.IsRestoreMap)
                {
                    if (stateManager.TileSaveData != null && stateManager.TileSaveData.Count > 0)
                    {
                        HexGridLayout layout = FindObjectOfType<HexGridLayout>();
                        layout.CalculateMapOrigin();
                        layout.RestoreMapState(stateManager.TileSaveData, stateManager.PlayerCoord);

                        StartCoroutine(MoveToClearedTile());
                    }
                }
            }

            private IEnumerator MoveToClearedTile()
            {
                yield return null;

                if (_choseTileData != null && _choseTileData.IsCleared)
                {
                    Vector3 targetPos = _hexGridLayout.GetPositionForHexFromCoordinate(_choseTileData.Coord);
                    _playerController.MoveTo(targetPos);
                    GameStateManager.Instance.ResetIsRestoreMap();
                }
            }
        }
    }
