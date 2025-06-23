using UnityEngine;

using ZL.Unity.Singleton;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Enemy Manager (Singleton)")]

    public sealed class EnemyManager : MonoSingleton<EnemyManager>
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Text("<b>���� �����ϴ� ��ǥ</b>")]

        private Transform destination = null;

        public Transform Destination
        {
            get => destination;
        }
    }
}