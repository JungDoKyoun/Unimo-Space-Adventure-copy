using System;

using UnityEngine;

namespace ZL.Unity.Pooling
{
    [AddComponentMenu("ZL/Pooling/Pooled Object")]

    public class PooledObject : MonoBehaviour
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Button(nameof(Appear))]

        [Button(nameof(Disappear))]

        [Margin]

        private float lifeTime = -1f;

        public float LifeTime
        {
            set => lifeTime = value;
        }

        public event Action OnDisableAction = null;

        private event Action OnCollectedAction = null;

        public static TClone Instantiate<TClone>(ObjectPool<TClone> objectPool)

            where TClone : PooledObject
        {
            var clone = Instantiate(objectPool.Prefab, objectPool.Parent);

            clone.OnCollectedAction += () => objectPool.Collect(clone);

            return clone;
        }

        protected virtual void OnDisable()
        {
            if (OnDisableAction != null)
            {
                OnDisableAction.Invoke();

                OnDisableAction = null;
            }

            if (OnCollectedAction != null)
            {
                OnCollectedAction.Invoke();
            }

            #if UNITY_EDITOR

            else
            {
                FixedDebug.LogWarning($"Game Object '{gameObject.name}' is a 'Pooled Object' but was not created from an 'Object Pool'.");
            }

            #endif
        }

        public virtual void Appear()
        {
            gameObject.SetActive(true);
        }

        public virtual void OnAppeared()
        {
            if (lifeTime != -1f)
            {
                Invoke(nameof(Disappear), lifeTime);

                lifeTime = -1f;
            }
        }

        public virtual void Disappear()
        {
            CancelInvoke(nameof(Disappear));

            OnDisappear();
        }

        protected virtual void OnDisappear()
        {
            OnDisappeared();
        }

        public void OnDisappeared()
        {
            gameObject.SetActive(false);
        }
    }
}