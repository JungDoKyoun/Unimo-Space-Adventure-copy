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

        [Text("<b>적이 추적하는 목표</b>")]

        private Transform destination = null;

        public Transform Destination
        {
            get => destination;
        }
    }
}