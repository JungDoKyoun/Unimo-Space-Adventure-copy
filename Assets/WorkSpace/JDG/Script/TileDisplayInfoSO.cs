using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JDG
{
    [CreateAssetMenu(fileName = "TileDisplayInfoSO", menuName = "SO/TileDisplayInfoSO", order = 0)]
    public class TileDisplayInfoSO : ScriptableObject
    {
        public TileType TileType;
        public string DisplayName;
        public Sprite DisplayIcon;

        [Header("타일 타입이 모드일때만 작성")]
        public string ModeName;
    }
}
