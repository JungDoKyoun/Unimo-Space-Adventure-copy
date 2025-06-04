using UnityEngine;

using ZL.CS.Singleton;

using ZL.Unity.Singleton;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Stage Data Manager (Singleton)")]

    public sealed class StageDataManager : MonoSingleton<StageDataManager>
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [ReadOnlyWhenPlayMode]

        private StageData stageData = null;

        [SerializeField]

        [UsingCustomProperty]

        [ReadOnlyWhenPlayMode]

        private RewardData rewardData = null;

        [SerializeField]

        [UsingCustomProperty]

        [ReadOnlyWhenPlayMode]

        private RelicDropData relicDropTable = null;

        protected override void Awake()
        {
            base.Awake();

            ISingleton<StageData>.TrySetInstance(stageData);

            ISingleton<RewardData>.TrySetInstance(rewardData);

            ISingleton<RelicDropData>.TrySetInstance(relicDropTable);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            ISingleton<StageData>.Release(stageData);

            ISingleton<RewardData>.Release(rewardData);

            ISingleton<RelicDropData>.Release(relicDropTable);
        }
    }
}