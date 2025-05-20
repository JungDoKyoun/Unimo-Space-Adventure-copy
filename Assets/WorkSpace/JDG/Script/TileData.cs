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
        private ModeType _modeType;
        private EventType _eventType;
        private int _level;

        public TileData(Vector2Int coord, TileType tileType, TileVisibility tileVisibility, EnvironmentType environmentType, bool isCleared, int level, string sceneName = "")
        {
            _coord = coord;
            _tileType = tileType;
            _tileVisibility = tileVisibility;
            _environmentType = environmentType;
            _isCleared = isCleared;
            _sceneName = sceneName;
            _level = level;
        }

        public Vector2Int Coord { get { return _coord; } set { _coord = value; } }
        public TileType TileType { get { return _tileType; } set { _tileType = value; } }
        public TileVisibility TileVisibility { get { return _tileVisibility; } set { _tileVisibility = value; } }
        public EnvironmentType EnvironmentType { get { return _environmentType; } set { _environmentType = value; } }
        public bool IsCleared { get { return _isCleared; } set { _isCleared = value; } }
        public string SceneName { get { return _sceneName; } set { _sceneName = value; } }
        public ModeType ModeType { get { return _modeType; } set { _modeType = value; } }
        public EventType EventType { get { return _eventType; } set { _eventType = value; } }
        public int Level { get { return _level; } set { _level = value; } }
    }
}
