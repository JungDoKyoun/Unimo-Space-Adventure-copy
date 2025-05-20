using System.Collections.Generic;
using UnityEngine;

namespace JDG
{
    public class RewardManager : MonoBehaviour
    {
        private static RewardManager _instance;
        [SerializeField] private List<TileRewardRuleSO> _tileRewardRuleSOs;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
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

        public List<RewardData> GetTileRewardRuleSO(TileType tileType, ModeType modeType)
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
    }
}
