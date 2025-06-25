using System.Collections;

using UnityEngine;

namespace ZL.Unity.Unimo
{
    public sealed class SkillSequence<TSkillUser>

        where TSkillUser : MonoBehaviour
    {
        private readonly Skill<TSkillUser>[] skills = null;

        public SkillSequence(params Skill<TSkillUser>[] skills)
        {
            this.skills = skills;
        }

        public void StartRoutine(TSkillUser skillUser)
        {
            if (routine != null)
            {
                return;
            }

            routine = Routine();

            skillUser.StartCoroutine(routine);
        }

        public void StopRoutine(TSkillUser skillUser)
        {
            if (routine == null)
            {
                return;
            }

            skillUser.StopCoroutine(routine);

            routine = null;
        }

        private IEnumerator routine = null;

        private IEnumerator Routine()
        {
            while (true)
            {
                if (MathfEx.CDF(skills, GetWeight, out int index) == true)
                {
                    skills[index].SetCooldownTimer();

                    yield return skills[index].Routine();
                }

                else
                {
                    yield return null;
                }
            }
        }

        public void Cooldown(float time)
        {
            for (int i = 0; i < skills.Length; ++i)
            {
                skills[i].Cooldown(time);
            }
        }

        private float GetWeight(Skill<TSkillUser> skill)
        {
            return skill.GetWeight();
        }
    }
}