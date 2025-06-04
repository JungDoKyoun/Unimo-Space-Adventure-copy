using UnityEngine;

using ZL.CS.Singleton;

using ZL.Unity.Singleton;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Data Sheet Manager")]

    public sealed class DataSheetManager : MonoSingleton<DataSheetManager>
    {
        [Space]

        [SerializeField]

        private bool updateDataOnAwake = false;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Button(nameof(UpdateData))]

        [Essential]

        [ReadOnlyWhenPlayMode]

        private EnemyDataSheet enemyDataSheet = null;

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [ReadOnlyWhenPlayMode]

        private GatheringDataSheet gatheringDataSheet = null;

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [ReadOnlyWhenPlayMode]

        private RelicDropDataSheet relicDropTableSheet = null;

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [ReadOnlyWhenPlayMode]

        private RewardDataSheet rewardDataSheet = null;

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [ReadOnlyWhenPlayMode]

        private SpawnPatternDataSheet spawnPatternDataSheet = null;

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [ReadOnlyWhenPlayMode]

        private SpawnerDataSheet spawnerDataSheet = null;

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [ReadOnlyWhenPlayMode]

        private StageDataSheet stageDataSheet = null;

        protected override void Awake()
        {
            base.Awake();

            ISingleton<EnemyDataSheet>.TrySetInstance(enemyDataSheet);

            ISingleton<GatheringDataSheet>.TrySetInstance(gatheringDataSheet);

            ISingleton<RelicDropDataSheet>.TrySetInstance(relicDropTableSheet);

            ISingleton<RewardDataSheet>.TrySetInstance(rewardDataSheet);

            ISingleton<SpawnPatternDataSheet>.TrySetInstance(spawnPatternDataSheet);

            ISingleton<SpawnerDataSheet>.TrySetInstance(spawnerDataSheet);

            ISingleton<StageDataSheet>.TrySetInstance(stageDataSheet);

            if (updateDataOnAwake == true)
            {
                UpdateData();
            }
        }

        public void UpdateData()
        {
            enemyDataSheet.Read();

            spawnerDataSheet.Read();

            spawnPatternDataSheet.Read();
        }
    }
}