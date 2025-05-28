using JDG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JDG
{
    [CreateAssetMenu(fileName = "RewardTableSO", menuName = "SO/EventSO/RewardTableSO")]
    public class RewardTableSO : ScriptableObject
    {
        public List<DifficultyRewardEntry> _rewardEntries;
    }

    [System.Serializable]
    public class DifficultyRewardEntry
    {
        public DifficultyType _difficultyType;
        public int _minReward;
        public int _maxReward;
    }
}
