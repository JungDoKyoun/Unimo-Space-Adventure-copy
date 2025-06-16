using UnityEngine;

using ZL.Unity.Pooling;

namespace ZL.Unity.Unimo
{
    public abstract class Item : PooledObject
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [Essential]

        [ReadOnly(true)]

        #pragma warning disable CS0108

        private Collider collider = null;

        #pragma warning restore CS0108

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponentInChildren]

        [ReadOnly(true)]

        protected Animator animator = null;

        [Space]

        [SerializeField]

        private float lifeTime = -1f;

        private void OnEnable()
        {
            if (lifeTime > 0f)
            {
                Invoke(nameof(Disappear), lifeTime);
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            if (animator != null)
            {
                animator.Rebind();
            }

            #if UNITY_EDITOR

            OnDisappear();

            #endif
        }

        public virtual void OnAppeared()
        {
            collider.enabled = true;
        }

        protected void Disappear()
        {
            OnDisappear();

            animator.SetTrigger("Disappear");
        }

        protected void OnDisappear()
        {
            collider.enabled = false;
        }

        public void OnDisappeared()
        {
            gameObject.SetActive(false);
        }

        public abstract void GetItem(PlayerManager player);
    }
}