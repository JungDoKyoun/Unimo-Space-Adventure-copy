using UnityEngine;

using ZL.CS.Singleton;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Data Sheet Manager")]

    [DefaultExecutionOrder((int)ScriptExecutionOrder.FastAwake)]

    public sealed class DataSheetManager : MonoBehaviour
    {
        [Space]

        [SerializeField]

        private EnemyDataSheet enemyDataSheet = null;

        [SerializeField]

        private GatheringDataSheet gatheringDataSheet = null;

        [SerializeField]

        [UsingCustomProperty]

        private RelicDataSheet relicDataSheet = null;

        [SerializeField]

        [UsingCustomProperty]

        private RelicDropTableSheet relicDropTableSheet = null;

        [SerializeField]

        private SpawnPatternDataSheet spawnPatternDataSheet = null;

        [SerializeField]

        private SpawnerDataSheet spawnerDataSheet = null;

        [SerializeField]

        private StageDataSheet stageDataSheet = null;

        [SerializeField]

        private StageRewardDataSheet stageRewardDataSheet = null;

        [SerializeField]

        private RelicEffectStringTableSheet relicEffectStringTableSheet = null;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Button(nameof(UpdateAllSheets))]

        private bool updateAllSheetsOnAwake = false;

        private void Awake()
        {
            ISingleton<EnemyDataSheet>.TrySetInstance(enemyDataSheet);

            ISingleton<GatheringDataSheet>.TrySetInstance(gatheringDataSheet);

            ISingleton<RelicDataSheet>.TrySetInstance(relicDataSheet);

            ISingleton<RelicDropTableSheet>.TrySetInstance(relicDropTableSheet);

            ISingleton<SpawnPatternDataSheet>.TrySetInstance(spawnPatternDataSheet);

            ISingleton<SpawnerDataSheet>.TrySetInstance(spawnerDataSheet);

            ISingleton<StageDataSheet>.TrySetInstance(stageDataSheet);

            ISingleton<StageRewardDataSheet>.TrySetInstance(stageRewardDataSheet);

            ISingleton<RelicEffectStringTableSheet>.TrySetInstance(relicEffectStringTableSheet);

            if (updateAllSheetsOnAwake == true)
            {
                UpdateAllSheets();
            }
        }

        private void OnDestroy()
        {
            ISingleton<EnemyDataSheet>.Release(enemyDataSheet);

            ISingleton<GatheringDataSheet>.Release(gatheringDataSheet);

            ISingleton<RelicDataSheet>.Release(relicDataSheet);

            ISingleton<RelicDropTableSheet>.Release(relicDropTableSheet);

            ISingleton<SpawnPatternDataSheet>.Release(spawnPatternDataSheet);

            ISingleton<SpawnerDataSheet>.Release(spawnerDataSheet);

            ISingleton<StageDataSheet>.Release(stageDataSheet);

            ISingleton<StageRewardDataSheet>.Release(stageRewardDataSheet);

            ISingleton<RelicEffectStringTableSheet>.Release(relicEffectStringTableSheet);
        }

        public void UpdateAllSheets()
        {
            enemyDataSheet?.Read();

            gatheringDataSheet?.Read();

            relicDataSheet?.Read();

            relicDropTableSheet?.Read();

            spawnPatternDataSheet?.Read();

            spawnerDataSheet?.Read();

            stageDataSheet?.Read();

            stageRewardDataSheet?.Read();

            relicEffectStringTableSheet?.Read();
        }
    }
}