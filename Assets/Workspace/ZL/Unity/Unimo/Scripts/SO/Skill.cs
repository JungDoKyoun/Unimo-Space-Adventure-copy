using System.Collections;

using UnityEngine;

namespace ZL.Unity.Unimo
{
    public abstract class Skill<TSkillUser>

        where TSkillUser : class
    {
        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        protected TSkillUser skillUser = null;

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        protected SkillData skillData = null;

        [Space]

        [SerializeField]

        private float cooldownTimer = 0f;

        public abstract IEnumerator Routine();

        public void SetCooldownTimer()
        {
            cooldownTimer = skillData.CooldownTime;
        }

        public void Cooldown(float deltaTime)
        {
            cooldownTimer -= deltaTime;

            if (cooldownTimer < 0f)
            {
                cooldownTimer = 0f;
            }
        }

        public virtual float GetWeight()
        {
            if (cooldownTimer > 0f)
            {
                return 0f;
            }

            return skillData.Weight;
        }
    }
}