using System.Collections;

namespace ZL.Unity.Unimo
{
    public sealed class SkillSequence<TSkillUser>

        where TSkillUser : class
    {
        private readonly Skill<TSkillUser>[] skills = null;

        public SkillSequence(params Skill<TSkillUser>[] skills)
        {
            this.skills = skills;
        }

        public void Cooldown(float deltaTime)
        {
            for (int i = 0; i < skills.Length; ++i)
            {
                skills[i].Cooldown(deltaTime);
            }
        }

        public IEnumerator Routine()
        {
            if (MathfEx.CDF(skills, GetWeight, out int index) == true)
            {
                skills[index].SetCooldownTimer();

                yield return skills[index].Routine();
            }
        }

        private float GetWeight(Skill<TSkillUser> skill)
        {
            return skill.GetWeight();
        }
    }
}