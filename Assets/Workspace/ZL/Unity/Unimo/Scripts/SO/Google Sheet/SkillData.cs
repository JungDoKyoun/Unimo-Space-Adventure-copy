using GoogleSheetsToUnity;

using System.Collections.Generic;

using UnityEngine;

using ZL.Unity.SO.GoogleSheet;

namespace ZL.Unity.Unimo
{
    [CreateAssetMenu(menuName = "ZL/Unimo/SO/Skill Data", fileName = "Skill Data")]

    public sealed class SkillData : ScriptableGoogleSheetData
    {
        [Space]

        [SerializeField]

        private float weight = 0f;

        public float Weight
        {
            get => weight;
        }

        [SerializeField]

        private float cooldownTime = 0f;

        public float CooldownTime
        {
            get => cooldownTime;
        }

        private float cooldownTimer = 0f;

        public float CooldownTimer
        {
            get => cooldownTimer;
        }

        [SerializeField]

        private float range = 0f;

        public float Range
        {
            get => range;
        }

        [SerializeField]

        private float power = 0f;

        public float Power
        {
            get => power;
        }

        [SerializeField]

        private float duration = 0f;

        public float Duration
        {
            get => duration;
        }

        public override List<string> GetHeaders()
        {
            return new List<string>()
            {
                nameof(name),

                nameof(weight),

                nameof(cooldownTime),

                nameof(range),

                nameof(power),

                nameof(duration),
            };
        }

        public override void Import(GstuSpreadSheet sheet)
        {
            weight = float.Parse(sheet[name, nameof(weight)].value);

            cooldownTime = float.Parse(sheet[name, nameof(cooldownTime)].value);

            range = float.Parse(sheet[name, nameof(range)].value);

            power = float.Parse(sheet[name, nameof(power)].value);

            duration = float.Parse(sheet[name, nameof(duration)].value);
        }

        public override List<string> Export()
        {
            return new List<string>()
            {
                name.ToString(),

                weight.ToString(),

                cooldownTime.ToString(),

                range.ToString(),

                power.ToString(),

                duration.ToString(),
            };
        }
    }
}