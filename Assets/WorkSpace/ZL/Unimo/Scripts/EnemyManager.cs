using UnityEngine;

using ZL.Unity.Singleton;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Enemy Manager")]

    public sealed class EnemyManager : MonoSingleton<EnemyManager>
    {
        [Space]

        [SerializeField]

        private bool updateDataOnAwake = false;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Button(nameof(UpdateData))]

        [ReadOnlyWhenPlayMode]

        private EnemyDataSheet enemyDataSheet = null;

        public Transform Target { get; set; } = null;

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
        }
    }
}