using UnityEngine;

namespace ZL.Unity.Unimo
{
    public abstract class Item : SpawnedObject
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponentInChildren]

        [ReadOnly(true)]

        protected AnimatorGroup animatorGroup = null;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [Essential]

        [ReadOnly(true)]

        protected Collider mainCollider = null;

        public override void OnAppeared()
        {
            mainCollider.enabled = true;

            base.OnAppeared();
        }

        public override void Disappear()
        {
            base.Disappear();

            mainCollider.enabled = false;
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

        public abstract void GetItem<TMonoBehaviour>(TMonoBehaviour getter)
            
            where TMonoBehaviour : MonoBehaviour;
    }
}