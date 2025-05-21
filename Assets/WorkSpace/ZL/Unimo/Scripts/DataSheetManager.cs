using UnityEngine;

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

        private SpawnerDataSheet spawnerDataSheet = null;

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [ReadOnlyWhenPlayMode]

        private SpawnPatternDataSheet spawnPatternDataSheet = null;

        protected override void Awake()
        {
            base.Awake();

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