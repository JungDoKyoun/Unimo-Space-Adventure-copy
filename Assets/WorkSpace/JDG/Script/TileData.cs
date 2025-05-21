using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

namespace JDG
{
    public class TileData
    {
        private Vector2Int _coord;
        private TileType _tileType;
        private TileVisibility _tileVisibility;
        private EnvironmentType _environmentType;
        private bool _isCleared;
        private int _level;
        private string _sceneName;
        private string _modeName;

        public TileData(Vector2Int coord, TileType tileType, TileVisibility tileVisibility, EnvironmentType environmentType, bool isCleared, int level, string sceneName = "", string modeName = "")
        {
            _coord = coord;
            _tileType = tileType;
            _tileVisibility = tileVisibility;
            _environmentType = environmentType;
            _isCleared = isCleared;
            _level = level;
            _sceneName = sceneName;
            _modeName = modeName;
        }

        public Vector2Int Coord { get { return _coord; } set { _coord = value; } }
        public TileType TileType { get { return _tileType; } set { _tileType = value; } }
        public TileVisibility TileVisibility { get { return _tileVisibility; } set { _tileVisibility = value; } }
        public EnvironmentType EnvironmentType { get { return _environmentType; } set { _environmentType = value; } }
        public bool IsCleared { get { return _isCleared; } set { _isCleared = value; } }
        public int Level { get { return _level; } set { _level = value; } }
        public string SceneName { get { return _sceneName; } set { _sceneName = value; } }
        public string ModeName { get { return _modeName; } set { _modeName = value; } }

        public Dictionary<string, object> ToDictionary() // TODO: 나중에 계정 별 진행도 저장 시 사용 됨
        {
            return new Dictionary<string, object>
            {
                { "CoordX", Coord.x },
                { "CoordY", Coord.y },
                { "TileType", (int)TileType }, // enum은 int로 변환
                { "TileVisibility", (int)TileVisibility },
                { "EnvironmentType", (int)EnvironmentType },
                { "IsCleared", IsCleared },
                { "Level", Level },
                { "SceneName", SceneName },
                { "ModeName", ModeName }
            };
        }

        public static TileData FromDictionary(Dictionary<string, object> dict) // TODO: 나중에 계정 별 진행도 불러올 때 사용 됨
        {
            Vector2Int coord = new Vector2Int(Convert.ToInt32(dict["CoordX"]), Convert.ToInt32(dict["CoordY"]));
            TileType tileType = (TileType)Convert.ToInt32(dict["TileType"]);
            TileVisibility visibility = (TileVisibility)Convert.ToInt32(dict["TileVisibility"]);
            EnvironmentType envType = (EnvironmentType)Convert.ToInt32(dict["EnvironmentType"]);
            bool isCleared = Convert.ToBoolean(dict["IsCleared"]);
            int level = Convert.ToInt32(dict["Level"]);
            string sceneName = dict["SceneName"]?.ToString();
            string modeName = dict["ModeName"]?.ToString();

            return new TileData(coord, tileType, visibility, envType, isCleared, level, sceneName, modeName);
        }
    }
}
