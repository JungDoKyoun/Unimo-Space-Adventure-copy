using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JDG
{
    public class TileData
    {
        private Vector2Int _coord;
        private TileType _tileType;
        private TileVisibility _tileVisibility;
        private EnvironmentType _environmentType;
        private bool _isCleared;
        private string _sceneName;
        private string _modeName;

        public TileData(Vector2Int coord, TileType tileType, TileVisibility tileVisibility, EnvironmentType environmentType, bool isCleared, string sceneName = "", string modeName = "")
        {
            _coord = coord;
            _tileType = tileType;
            _tileVisibility = tileVisibility;
            _environmentType = environmentType;
            _isCleared = isCleared;
            _sceneName = sceneName;
            _modeName = modeName;
        }

        public Vector2Int Coord { get { return _coord; } set { _coord = value; } }
        public TileType TileType { get { return _tileType; } set { _tileType = value; } }
        public TileVisibility TileVisibility { get { return _tileVisibility; } set { _tileVisibility = value; } }
        public EnvironmentType EnvironmentType { get { return _environmentType; } set { _environmentType = value; } }
        public bool IsCleared { get { return _isCleared; } set { _isCleared = value; } }
        public string SceneName { get { return _sceneName; } set { _sceneName = value; } }
        public string ModeName { get { return _modeName; } set { _modeName = value; } }
    }
}
