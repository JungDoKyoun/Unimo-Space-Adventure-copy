using GoogleSheetsToUnity;

using System.Collections.Generic;

using UnityEngine;

using ZL.Unity.IO.GoogleSheet;

namespace ZL.Unity.Unimo
{
    [CreateAssetMenu(menuName = "ZL/Unimo/Monster Data", fileName = "Monster Data")]

    public sealed class MonsterData : ScriptableSheetData
    {
        [Space]

        [SerializeField]

        // 최대체력
        private int maxHealth = 0;

        public int MaxHealth
        {
            get => maxHealth;
        }

        // 현재체력
        //private int currentHealth = 0;

        [SerializeField]

        // 이동속도
        private float moveSpeed = 0f;

        public float MoveSpeed
        {
            get => moveSpeed;
        }

        [SerializeField]

        // 공격력
        private int attackPower = 0;

        public int AttackPower
        {
            get => attackPower;
        }

        [SerializeField]

        // 경직시간
        private float staggerDuration = 0f;

        public float StaggerDuration
        {
            get => staggerDuration;
        }

        [SerializeField]

        //넉백거리
        private float knockbackDistance = 0f;

        public float KnockbackDistance
        {
            get => knockbackDistance;
        }

        public override List<string> GetHeader()
        {
            return new List<string>()
            {
                "name",

                nameof(maxHealth),

                nameof(moveSpeed),

                nameof(attackPower),

                nameof(staggerDuration),

                nameof(knockbackDistance),
            };
        }

        public override void Import(GstuSpreadSheet sheet)
        {
            maxHealth = int.Parse(sheet[name, nameof(maxHealth)].value);

            moveSpeed = float.Parse(sheet[name, nameof(moveSpeed)].value);

            attackPower = int.Parse(sheet[name, nameof(attackPower)].value);

            staggerDuration = float.Parse(sheet[name, nameof(staggerDuration)].value);

            knockbackDistance = float.Parse(sheet[name, nameof(knockbackDistance)].value);
        }

        public override List<string> Export()
        {
            return new List<string>()
            {
                name,

                maxHealth.ToString(),

                moveSpeed.ToString(),

                attackPower.ToString(),

                staggerDuration.ToString(),

                knockbackDistance.ToString(),
            };
        }
    }
}