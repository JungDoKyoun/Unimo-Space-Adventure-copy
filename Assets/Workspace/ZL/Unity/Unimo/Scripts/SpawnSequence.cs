using UnityEngine;

using ZL.Unity.Singleton;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Spawn Sequence (Singleton)")]

    public sealed class SpawnSequence : MonoSingleton<SpawnSequence>
    {
        protected override void Awake()
        {
            base.Awake();

            gameObject.SetActive(false);
        }
    }
}