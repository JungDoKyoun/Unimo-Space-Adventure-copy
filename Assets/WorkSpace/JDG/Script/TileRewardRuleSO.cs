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
        public List<RewardData> RewardDatas;

        [Header("타일 타입이 모드일때만 작성")]
        public string ModeName;
    }
}
