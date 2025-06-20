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

        protected override void OnDisable()
        {
            base.OnDisable();

            if (animator != null)
            {
                animator.Rebind();
            }
        }

        public override void OnAppeared()
        {
            collider.enabled = true;

            base.OnAppeared();
        }

        public override void Disappear()
        {
            base.Disappear();

            collider.enabled = false;
        }

        protected override void OnDisappear()
        {
            animator.SetTrigger("Disappear");
        }

        public abstract void GetItem(PlayerManager player);
    }
}