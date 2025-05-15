using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JDG
{
    public class TileDisplayInfoManager : MonoBehaviour
    {
        private static TileDisplayInfoManager _instance;
        [SerializeField] private List<TileDisplayInfoSO> _tileDisplayInfoSOs;

        private void Awake()
        {
            if(_instance == null)
            {
                _instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public static TileDisplayInfoManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<TileDisplayInfoManager>();
                }
                return _instance;
            }
        }

        public TileDisplayInfoSO GetDisplayInfo(TileType tileType, string modeName)
        {
            foreach(var data in _tileDisplayInfoSOs)
            {
                if (data.TileType != tileType)
                    continue;
                if (data.TileType == TileType.Mode && data.ModeName != modeName)
                    continue;

                return data;
            }
            return null;
        }
    }
}
