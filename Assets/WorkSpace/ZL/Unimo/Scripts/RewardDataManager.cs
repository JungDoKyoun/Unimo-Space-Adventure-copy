using System.Collections.Generic;

using UnityEngine;

using ZL.Unity.Singleton;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Reward Data Manager")]

    public sealed class RewardDataManager : MonoSingleton<RewardDataManager>
    {
        [Space]

        [SerializeField]

        private RewardDataSheet rewardDataSheet = null;

        public Dictionary<string, RewardData> Datas
        {
            get => rewardDataSheet.DataDictionary;
        }
    }
}