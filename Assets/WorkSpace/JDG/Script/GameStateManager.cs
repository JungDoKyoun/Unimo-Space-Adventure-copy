using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JDG;

namespace JDG
{
    public class GameStateManager : MonoBehaviour
    {
        private static GameStateManager _instance;
        private Dictionary<Vector2Int, TileData> _tileSaveData = new Dictionary<Vector2Int, TileData>();
        private Vector2Int _playerCoord;
        private static bool _isRestoreMap = false;
        private static bool _isClear = false;

        private void Awake()
        {
            if(_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public static GameStateManager Instance
        {
            get
            {
                return _instance;
            }
        }
        public Dictionary<Vector2Int, TileData> TileSaveData { get { return _tileSaveData; } }
        public Vector2Int PlayerCoord { get { return _playerCoord; } }
        public static bool IsRestoreMap { get { return _isRestoreMap; } set { _isRestoreMap = value; } }
        public static bool IsClear { get { return _isClear; } set { _isClear = value; } }

        public void SaveTileStates(Dictionary<Vector2Int, HexRenderer> tileData, Vector2Int playerCoord)
        {
            _isRestoreMap = true;
            ClearSaveData();

            foreach(var data in tileData)
            {
                Vector2Int coord = data.Key;
                TileData tiledata = data.Value.TileData;

                _tileSaveData[coord] = new TileData(tiledata.Coord, tiledata.TileType, tiledata.TileVisibility, tiledata.EnvironmentType, tiledata.IsCleared ,tiledata.DifficultyType, tiledata.SceneName);

                if (tiledata.TileType == TileType.Mode)
                    _tileSaveData[coord].ModeType = tiledata.ModeType;

                else if (tiledata.TileType == TileType.Event)
                    _tileSaveData[coord].EventType = tiledata.EventType;
            }
            _playerCoord = playerCoord;
        }

        public void ClearSaveData()
        {
            _tileSaveData.Clear();
        }

        public void ResetIsRestoreMap()
        {
            _isRestoreMap = false;
            _isClear = false;
        }

        public void UpdateTileState(TileData tileData)
        {
            if(tileData != null)
            {
                _tileSaveData[tileData.Coord] = tileData;
            }
        }
    }
}
