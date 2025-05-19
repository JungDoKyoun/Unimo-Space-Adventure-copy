using UnityEngine;

using ZL.CS.Singleton;

namespace ZL.Unity.Singleton
{
    [DefaultExecutionOrder((int)ScriptExecutionOrder.Singleton)]

    public abstract class MonoSingleton<TMonoSingleton> : MonoBehaviour, ISingleton<TMonoSingleton>

        where TMonoSingleton : MonoSingleton<TMonoSingleton>
    {
        public static TMonoSingleton Instance
        {
            get => ISingleton<TMonoSingleton>.Instance;
        }

        protected virtual void Awake()
        {
            ISingleton<TMonoSingleton>.TrySetInstance((TMonoSingleton)this);
        }

        protected virtual void OnDestroy()
        {
            ISingleton<TMonoSingleton>.Release((TMonoSingleton)this);
        }
    }
}