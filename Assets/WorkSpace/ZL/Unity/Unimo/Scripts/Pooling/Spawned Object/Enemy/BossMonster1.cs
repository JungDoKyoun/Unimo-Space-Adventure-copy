using System.Collections;
using UnityEngine;

using UnityEngine.Animations;
using ZL.Unity.Coroutines;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Boss Monster 1 (Spawned)")]

    public sealed class BossMonster1 : Enemy, IDamager
    {
        [Space]

        [SerializeField]

        private GameObject hitVFX = null;

        [Space]

        [SerializeField]

        private string projectileName = "";

        [Space]

        [SerializeField]

        private float dashCooldown = 0f;

        [SerializeField]

        private float dashRange = 0f;

        [SerializeField]

        private float dashDuration = 0f;

        [SerializeField]

        private float dashSpeedMultiply = 0f;

        private bool isDashing = false;

        public override void OnAppeared()
        {
            base.OnAppeared();

            StartCoroutine(BossPatternRoutine());
        }

        private IEnumerator BossPatternRoutine()
        {
            while (true)
            {
                yield return WaitForFixedUpdateCache.Get();
            }
        }

        private IEnumerator BossPattern1()
        {
            yield return WaitForFixedUpdateCache.Get();
        }

        private void FixedUpdate()
        {
            if (isStoped == true)
            {
                return;
            }

            float movementSpeed = enemyData.MovementSpeed;

            if (isDashing == false)
            {
                if (rotationSpeed != 0f)
                {
                    rigidbody.LookTowards(Destination.position, rotationSpeed * Time.fixedDeltaTime, Axis.Y);
                }
            }

            else
            {
                movementSpeed *= dashSpeedMultiply;
            }

            if (movementSpeed != 0f)
            {
                rigidbody.MoveForward(movementSpeed * Time.fixedDeltaTime);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Item"))
            {

            }
        }

        public override void TakeDamage(float damage, Vector3 contact)
        {
            hitVFX.transform.LookAt(contact, Axis.Y);

            hitVFX.SetActive(true);

            base.TakeDamage(damage, contact);
        }

        public void GiveDamage(IDamageable damageable, Vector3 contact)
        {
            damageable.TakeDamage(enemyData.AttackPower, contact);
        }
    }
}