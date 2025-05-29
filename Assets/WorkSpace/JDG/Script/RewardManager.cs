using System.Collections.Generic;
using UnityEngine;
using JDG;

namespace JDG
{
    public class RewardManager : MonoBehaviour
    {
        private static RewardManager _instance;
        [SerializeField] private List<TileRewardRuleSO> _tileRewardRuleSOs;
        [SerializeField] private RewardTableSO _rewardTableSO;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public static RewardManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<RewardManager>();
                }
                return _instance;
            }
        }

        public List<RewardData> GetTileRewards(TileType tileType, ModeType modeType, DifficultyType difficultyType)
        {
            List<RewardData> datas = GetTileRewardRuleSO(tileType, modeType);
            List<RewardData> result = new List<RewardData>();

            foreach(RewardData data in datas)
            {
                RewardData copy = new RewardData
                {
                    _rewardType = data._rewardType,
                    _resourceData = data._resourceData,
                    _relicData = data._relicData,
                    _rewardAmount = 0
                };

                result.Add(copy);
            }
            return result;
        }

        private int GetRandomRewardAmount(DifficultyType difficulty, ResourcesType resourcesType)
        {
            var entry =_rewardTableSO._rewardEntries.Find(e => e._difficultyType == difficulty);
            if (entry == null)
                return 1;

            var reward = entry._rewardAmounts.Find(e => e._resourcesType == resourcesType);
            if (reward == null)
                return 1;

            return Random.Range(reward._minReward, reward._maxReward);
        }

        private List<RewardData> GetTileRewardRuleSO(TileType tileType, ModeType modeType)
        {
            foreach(TileRewardRuleSO data in _tileRewardRuleSOs)
            {
                if (data.TileType != tileType)
                    continue;
                if (tileType == TileType.Mode && modeType != data.ModeType)
                    continue;

                return data.RewardDatas;
            }
            return new List<RewardData>();
        }

        public Vector2Int GetRewardRange(DifficultyType difficulty, ResourcesType resourcesType)
        {
            var entry = _rewardTableSO._rewardEntries.Find(e => e._difficultyType == difficulty);
            if(entry == null)
            {
                return new Vector2Int(1, 1);
            }

            var reward = entry._rewardAmounts.Find(e => e._resourcesType == resourcesType);
            if (reward == null)
            {
                return new Vector2Int(1, 1);
            }

            return new Vector2Int(reward._minReward, reward._maxReward);
        }
    }
}
