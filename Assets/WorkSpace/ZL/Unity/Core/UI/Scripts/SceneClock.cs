using UnityEngine;

using ZL.CS.Singleton;

namespace ZL.Unity
{
    [AddComponentMenu("ZL/Scene Clock (Singleton)")]

    public sealed class SceneClock : Clock, ISingleton<SceneClock>
    {
        public static SceneClock Instance
        {
            get => ISingleton<SceneClock>.Instance;
        }

        private void Awake()
        {
            ISingleton<SceneClock>.TrySetInstance(this);
        }

        private void OnDestroy()
        {
            ISingleton<SceneClock>.Release(this);
        }
    }
}