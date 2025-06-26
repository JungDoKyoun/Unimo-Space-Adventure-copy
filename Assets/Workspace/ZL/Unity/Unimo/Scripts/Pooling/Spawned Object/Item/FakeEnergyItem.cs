using UnityEngine;

using ZL.Unity.Phys;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Fake Energy Item (Spawned)")]

    public sealed class FakeEnergyItem : Item
    {
        [Space]

        [SerializeField]

        private float damage = 0f;

        public override void GetItem<TMonoBehaviour>(TMonoBehaviour getter)
        {
            if (getter is IDamageable damageable)
            {
                var contact = mainCollider.ClosestPoint(damageable.MainCollider);

                damageable.TakeDamage(damage, contact);
            }

            Disappear();
        }
    }
}