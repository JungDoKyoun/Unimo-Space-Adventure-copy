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

        [UsingCustomProperty]

        [Button(nameof(UpdateData))]

        private bool updateDataOnAwake = false;

        [Space]

        [SerializeField]

        [Essential]

        private EnemyDataSheet enemyDataSheet = null;

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        private GatheringDataSheet gatheringDataSheet = null;

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        private RelicDropTableSheet relicDropTableSheet = null;

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        private RewardDataSheet rewardDataSheet = null;

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        private SpawnPatternDataSheet spawnPatternDataSheet = null;

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        private SpawnerDataSheet spawnerDataSheet = null;

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        private StageDataSheet stageDataSheet = null;

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        private StringTableSheet stringTableSheet = null;

        protected override void Awake()
        {
            base.Awake();

            ISingleton<EnemyDataSheet>.TrySetInstance(enemyDataSheet);

            ISingleton<GatheringDataSheet>.TrySetInstance(gatheringDataSheet);

            ISingleton<RelicDropTableSheet>.TrySetInstance(relicDropTableSheet);

            ISingleton<RewardDataSheet>.TrySetInstance(rewardDataSheet);

            ISingleton<SpawnPatternDataSheet>.TrySetInstance(spawnPatternDataSheet);

            ISingleton<SpawnerDataSheet>.TrySetInstance(spawnerDataSheet);

            ISingleton<StageDataSheet>.TrySetInstance(stageDataSheet);

            ISingleton<StringTableSheet>.TrySetInstance(stringTableSheet);

            if (updateDataOnAwake == true)
            {
                UpdateData();
            }
        }

        public void UpdateData()
        {
            enemyDataSheet.Read();

            gatheringDataSheet.Read();

            relicDropTableSheet.Read();

            rewardDataSheet.Read();

            spawnPatternDataSheet.Read();

            spawnerDataSheet.Read();

            stageDataSheet.Read();

            stringTableSheet.Read();
        }
    }
}