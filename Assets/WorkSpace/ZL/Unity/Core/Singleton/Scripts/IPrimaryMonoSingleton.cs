using UnityEngine;

using ZL.CS.Singleton;

namespace ZL.Unity.Singleton
{
    public interface IPrimaryMonoSingleton<TMonoBehaviour> : ISingleton<TMonoBehaviour>

        where TMonoBehaviour : MonoBehaviour, ISingleton<TMonoBehaviour>
    {
        bool ISingleton<TMonoBehaviour>.TrySetInstance()
        {
            var instance = (TMonoBehaviour)this;

            if (IsDuplicated() == true)
            {
                instance.gameObject.Destroy();

                return false;
            }

            Instance = instance;

            instance.DontDestroyOnLoad();

            return true;
        }
    }
}