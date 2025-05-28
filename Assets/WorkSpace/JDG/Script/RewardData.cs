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

        [Header("������ ��� �߰�")]
        public ResourceDataSO _resourceData;

        [Header("������ ��� �߰�")]
        public List<RelicDataSO> _relicDatas;
    }
}
