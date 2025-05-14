using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JDG;

namespace JDG
{
    [CreateAssetMenu(fileName = "TileRewardRuleSO", menuName = "SO/TileRewardRuleSO")]
    public class TileRewardRuleSO : ScriptableObject
    {
        public TileType TileType;
        public string ModeName;
        public List<RewardData> RewardDatas;
    }
}
