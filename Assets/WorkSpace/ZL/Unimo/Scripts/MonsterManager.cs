using UnityEngine;

using ZL.Unity.Singleton;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Monster Manager")]

    public sealed class MonsterManager : MonoSingleton<MonsterManager>
    {
        public Transform Target { get; set; } = null;

        /*[Space]

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [ReadOnlyWhenPlayMode]

        private MonsterDataSheet monsterDataSheet = null;

        protected override void Awake()
        {
            base.Awake();

            monsterDataSheet.Read();
        }*/

    }
}