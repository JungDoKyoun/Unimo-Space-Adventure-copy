using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JDG;

namespace JDG
{   
    public enum RewardType
    {
        Resource, Relic
    }

    [System.Serializable]
    public class RewardData
    {
        public RewardType _rewardType;
        public int _rewardAmount;

        [Header("유물일 경우 추가")]
        public ResourceDataSO _resourceData;

        [Header("유물일 경우 추가")]
        public List<RelicDataSO> _relicDatas;
    }
}
