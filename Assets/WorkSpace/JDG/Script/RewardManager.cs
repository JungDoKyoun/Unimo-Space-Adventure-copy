using System.Collections.Generic;
using UnityEngine;

namespace JDG
{
    public class RewardManager : MonoBehaviour
    {
        private RewardManager _instance;
        [SerializeField] private List<TileRewardRuleSO> _tileRewardRuleSOs;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
        }

        public RewardManager Instance
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

        public List<RewardData> GetTileRewardRuleSO(TileType tileType, string modeName)
        {
            foreach(TileRewardRuleSO data in _tileRewardRuleSOs)
            {
                if (data.TileType != tileType)
                    continue;
                if (tileType == TileType.Mode && modeName != data.ModeName)
                    continue;

                return data.RewardDatas;
            }
            return new List<RewardData>();
        }
    }
}
