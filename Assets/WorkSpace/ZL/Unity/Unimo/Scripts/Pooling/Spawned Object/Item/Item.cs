using UnityEngine;

namespace ZL.Unity.Unimo
{
    public abstract class Item : SpawnedObject
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

        protected AnimatorGroup animatorGroup = null;

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
            animatorGroup.SetTrigger("Disappear");
        }

        public override void OnDisappeared()
        {
            base.OnDisappeared();

            if (animatorGroup != null)
            {
                animatorGroup.Rebind();
            }
        }

        public abstract void GetItem<T>(T getter);
    }
}