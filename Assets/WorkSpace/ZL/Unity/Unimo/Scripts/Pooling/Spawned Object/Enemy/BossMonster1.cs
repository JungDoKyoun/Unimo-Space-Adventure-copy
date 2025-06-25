using System;

using System.Collections;

using UnityEngine;

using UnityEngine.Animations;

using ZL.Unity.Coroutines;

using ZL.Unity.Pooling;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Boss Monster 1 (Spawned)")]

    public sealed class BossMonster1 : Enemy, IDamager, IEnergizer
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        private GameObject hitVFX = null;

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        private Transform muzzle = null;

        [Space]

        [SerializeField]

        private string projectileName = "";

        [Space]

        [SerializeField]

        private DashSkill dashSkill = null;

        [Space]

        [SerializeField]

        private EnergyBoltSkill energyBoltskill = null;

        private int energy = 0;

        public int Energy
        {
            get => energy;

            set => energy = value;
        }

        private SkillSequence<BossMonster1> skillSequence = null;

        protected override void Awake()
        {
            base.Awake();

            skillSequence = new(dashSkill, energyBoltskill);
        }

        private void Update()
        {
            skillSequence.Cooldown(Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Item"))
            {
                var item = other.GetComponent<Item>();

                item.GetItem(this);
            }
        }

        public override void OnAppeared()
        {
            base.OnAppeared();

            skillSequence.StartRoutine(this);
        }

        public override void Disappear()
        {
            base.Disappear();

            skillSequence.StopRoutine(this);
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

        public void GetEnergy(int value)
        {
            Energy += value;

            energyBoltskill.Cooldown();
        }

        [Serializable]

        public sealed class DashSkill : Skill<BossMonster1>
        {
            public override IEnumerator Routine()
            {
                skillUser.rotationSpeed = 0f;

                skillUser.movementSpeed *= skillData.Power;

                yield return WaitForSecondsCache.Get(skillData.Duration);

                skillUser.rotationSpeed = skillUser.enemyData.RotationSpeed;

                skillUser.movementSpeed = skillUser.enemyData.MovementSpeed;
            }
        }

        [Serializable]

        public sealed class EnergyBoltSkill : Skill<BossMonster1>
        {
            [Space]

            [SerializeField]

            [UsingCustomProperty]

            [Essential]

            private Transform muzzle = null;

            [Space]

            [SerializeField]

            [UsingCustomProperty]

            [Essential]

            private string projectileName = "";

            [SerializeField]

            [UsingCustomProperty]

            [Essential]

            [Alias("Projectile Name (Enhanced)")]

            private string projectileName_Enhanced = "";

            public override float GetWeight()
            {
                if (skillUser.IsWithinRange(skillUser.Destination.position, skillData.Range) == false)
                {
                    return 0f;
                }

                return base.GetWeight();
            }

            public override IEnumerator Routine()
            {
                EnemyProjectile projectile;

                if (skillUser.energy > 0)
                {
                    --skillUser.energy;

                    projectile = ObjectPoolManager.Instance.Clone<EnemyProjectile>(projectileName_Enhanced);
                }

                else
                {
                    projectile = ObjectPoolManager.Instance.Clone<EnemyProjectile>(projectileName);
                }

                projectile.transform.SetPositionAndRotation(muzzle);

                projectile.LifeTime = skillData.Duration;

                projectile.Appear();

                yield return null;
            }
        }
    }
}