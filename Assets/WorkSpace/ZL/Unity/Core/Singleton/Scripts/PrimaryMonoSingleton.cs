using UnityEngine;

using ZL.CS.Singleton;

namespace ZL.Unity.Singleton
{
    [DefaultExecutionOrder((int)ScriptExecutionOrder.Singleton)]

    public abstract class PrimaryMonoSingleton<TPrimaryMonoSingleton> : MonoBehaviour, IPrimaryMonoSingleton<TPrimaryMonoSingleton>

        where TPrimaryMonoSingleton : PrimaryMonoSingleton<TPrimaryMonoSingleton>
    {
        public static TPrimaryMonoSingleton Instance
        {
            get => ISingleton<TPrimaryMonoSingleton>.Instance;
        }

        protected virtual void Awake()
        {
            ISingleton<TPrimaryMonoSingleton>.TrySetInstance((TPrimaryMonoSingleton)this);
        }

        protected virtual void OnDestroy()
        {
            ISingleton<TPrimaryMonoSingleton>.Release((TPrimaryMonoSingleton)this);
        }

        bool ISingleton<TPrimaryMonoSingleton>.IsDuplicated()
        {
            if (Instance == null)
            {
                return false;
            }

            return true;
        }
    }
}