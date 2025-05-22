using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JDG;

namespace JDG
{
    [CreateAssetMenu(fileName = "TileDisplayInfoSO", menuName = "SO/TileSO/TileDisplayInfoSO", order = 0)]
    public class TileDisplayInfoSO : ScriptableObject
    {
        public TileType TileType;
        public string DisplayName;
        public Sprite DisplayIcon;

        [Header("Ÿ�� Ÿ���� ����϶��� �ۼ�")]
        public ModeType ModeType;
    }
}
