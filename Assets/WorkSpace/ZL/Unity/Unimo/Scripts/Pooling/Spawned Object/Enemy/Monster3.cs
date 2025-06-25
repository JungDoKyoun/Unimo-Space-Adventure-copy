using UnityEngine;

using ZL.Unity.Phys;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Monster 3 (Spawned)")]

    public sealed class Monster3 : Enemy, IDamager
    {
        [Space]

        [SerializeField]

        private float dashSpeedMultiplier = 0f;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [ReadOnlyWhenPlayMode]

        private ArcedDetector detector = null;

        private bool isDashing = false;

        private void Update()
        {
            if (detector.Detect(Destination) == false)
            {
                return;
            }

            movementSpeed = 0f;

            detector.enabled = false;

            animatorGroup.SetTrigger("Encounter");
        }

        public override void OnAppeared()
        {
            detector.enabled = true;

            base.OnAppeared();
        }

        public override void Disappear()
        {
            base.Disappear();

            detector.enabled = false;
        }

        protected override void OnDisappear()
        {
            if (isDashing == true)
            {
                animatorGroup.SetTrigger("DashToDisappear");
            }

            else
            {
                animatorGroup.SetTrigger("Disappear");
            }
        }

        public override void OnDisappeared()
        {
            base.OnDisappeared();

            isDashing = false;
        }

        public void GiveDamage(IDamageable damageable, Vector3 contact)
        {
            damageable.TakeDamage(enemyData.AttackPower, contact);
        }

        public void Dash()
        {
            movementSpeed = enemyData.MovementSpeed * dashSpeedMultiplier;
        }
    }
}