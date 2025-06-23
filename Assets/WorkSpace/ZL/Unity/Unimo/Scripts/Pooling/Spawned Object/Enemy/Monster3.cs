using UnityEngine;

using UnityEngine.Animations;

using ZL.Unity.Phys;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Monster 3 (Spawned)")]

    public sealed class Monster3 : Enemy, IDamager
    {
        [Space]

        [SerializeField]

        private float dashSpeedMultiply = 0f;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [ReadOnlyWhenPlayMode]

        private ArcedDetector detector = null;

        private bool isDashing = false;

        private void FixedUpdate()
        {
            if (isStoped == true)
            {
                return;
            }

            if (rotationSpeed != 0f)
            {
                rigidbody.LookTowards(Destination.position, rotationSpeed * Time.fixedDeltaTime, Axis.Y);
            }

            if (enemyData.MovementSpeed != 0f)
            {
                float movementSpeed = enemyData.MovementSpeed;

                if (isDashing == true)
                {
                    movementSpeed *= dashSpeedMultiply;
                }

                rigidbody.MoveForward(movementSpeed * Time.fixedDeltaTime);
            }

            CheckDespawnCondition();
        }

        private void Update()
        {
            if (detector.Detect(Destination) == false)
            {
                return;
            }

            isStoped = true;

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

            isDashing = false;
        }

        public void Dash()
        {
            isStoped = false;

            isDashing = true;
        }

        public void GiveDamage(IDamageable damageable, Vector3 contact)
        {
            damageable.TakeDamage(enemyData.AttackPower, contact);
        }
    }
}