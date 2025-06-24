using System;

using System.Collections;

using UnityEngine;

using UnityEngine.Animations;

using ZL.Unity.Coroutines;

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

        private Skill2 skill2 = null;

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

            skillSequence = new(dashSkill, skill2);
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

            StartCoroutine(SkillSequenceRoutine());
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
        }

        private IEnumerator SkillSequenceRoutine()
        {
            while (true)
            {
                yield return skillSequence.Routine();
            }
        }

        [Serializable]

        public sealed class DashSkill : Skill<BossMonster1>
        {
            public override IEnumerator Routine()
            {
                Debug.Log("스킬 1 사용 중");

                skillUser.rotationSpeed = 0f;

                skillUser.movementSpeed *= skillData.Power;

                yield return WaitForSecondsCache.Get(skillData.Duration);

                skillUser.rotationSpeed = skillUser.enemyData.RotationSpeed;

                skillUser.movementSpeed = skillUser.enemyData.MovementSpeed;

                Debug.Log("스킬 1 사용 완료");
            }
        }

        [Serializable]

        public sealed class Skill2 : Skill<BossMonster1>
        {
            public override IEnumerator Routine()
            {
                Debug.Log("스킬 2 사용 중");

                skillUser.rotationSpeed = 0f;

                skillUser.movementSpeed = 0f;

                yield return WaitForSecondsCache.Get(skillData.Duration);

                skillUser.rotationSpeed = skillUser.enemyData.RotationSpeed;

                skillUser.movementSpeed = skillUser.enemyData.MovementSpeed;

                Debug.Log("스킬 2 사용 완료");
            }
        }
    }
}