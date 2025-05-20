using UnityEngine;

using ZL.Unity.Singleton;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Monster Manager")]

    public sealed class MonsterManager : MonoSingleton<MonsterManager>
    {
        [Space]

        [SerializeField]

        private bool updateDataOnAwake = false;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Button(nameof(UpdateData))]

        [ReadOnlyWhenPlayMode]

        private MonsterDataSheet monsterDataSheet = null;

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
            monsterDataSheet.Read();
        }
    }
}