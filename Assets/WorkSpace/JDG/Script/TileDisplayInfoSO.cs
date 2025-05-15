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

        [Header("Ÿ�� Ÿ���� ����϶��� �ۼ�")]
        public string ModeName;
    }
}
