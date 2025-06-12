using UnityEngine;

using ZL.CS.Singleton;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Data Sheet Manager")]

    [DefaultExecutionOrder((int)ScriptExecutionOrder.Singleton)]

    public sealed class DataSheetManager : MonoBehaviour
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Button(nameof(UpdateAllSheets))]

        [Margin]

        private bool updateAllSheetsOnAwake = false;

        [Space]

        [SerializeField]

        private EnemyDataSheet enemyDataSheet = null;

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

        private StageDataSheet[] stageDataSheets = null;

        [SerializeField]

        private StageRewardDataSheet[] stageRewardDataSheets = null;

        [SerializeField]

        private RelicEffectStringTableSheet relicEffectStringTableSheet = null;

        private void Awake()
        {
            ISingleton<RelicDataSheet>.TrySetInstance(relicDataSheet);

            ISingleton<RelicDropTableSheet>.TrySetInstance(relicDropTableSheet);

            ISingleton<RelicEffectStringTableSheet>.TrySetInstance(relicEffectStringTableSheet);

            ISingleton<SpawnPatternDataSheet>.TrySetInstance(spawnPatternDataSheet);

            ISingleton<SpawnerDataSheet>.TrySetInstance(spawnerDataSheet);

            if (updateAllSheetsOnAwake == true)
            {
                UpdateAllSheets();
            }
        }

        private void OnDestroy()
        {
            ISingleton<RelicDataSheet>.Release(relicDataSheet);

            ISingleton<RelicDropTableSheet>.Release(relicDropTableSheet);

            ISingleton<RelicEffectStringTableSheet>.Release(relicEffectStringTableSheet);

            ISingleton<SpawnPatternDataSheet>.Release(spawnPatternDataSheet);

            ISingleton<SpawnerDataSheet>.Release(spawnerDataSheet);
        }

        public void UpdateAllSheets()
        {
            if (enemyDataSheet != null)
            {
                enemyDataSheet.Read();
            }

            if (relicDataSheet != null)
            {
                relicDataSheet.Read();
            }

            if (relicDropTableSheet != null)
            {
                relicDropTableSheet.Read();
            }

            if (relicEffectStringTableSheet != null)
            {
                relicEffectStringTableSheet.Read();
            }

            if (spawnPatternDataSheet != null)
            {
                spawnPatternDataSheet.Read();
            }

            if (spawnerDataSheet != null)
            {
                spawnerDataSheet.Read();
            }

            for (int i = 0; i < stageDataSheets.Length; ++i)
            {
                stageDataSheets[i].Read();
            }

            for (int i = 0; i < stageRewardDataSheets.Length; ++i)
            {
                stageRewardDataSheets[i].Read();
            }
        }
    }
}