using GoogleSheetsToUnity;

using System.Collections.Generic;

using UnityEngine;

using ZL.Unity.SO.GoogleSheet;

namespace ZL.Unity.Unimo
{
    [CreateAssetMenu(menuName = "ZL/Unimo/SO/Enemy Data", fileName = "Enemy Data 1")]

    public sealed class EnemyData : ScriptableGoogleSheetData
    {
        [Space]

        [SerializeField]

        private float maxHealth = 0f;

        public float MaxHealth
        {
            get => maxHealth;
        }

        [SerializeField]

        private float rotationSpeed = 0f;

        public float RotationSpeed
        {
            get => rotationSpeed;
        }

        [SerializeField]

        private float movementSpeed = 0f;

        public float MovementSpeed
        {
            get => movementSpeed;
        }

        [SerializeField]

        private float attackPower = 0;

        public float AttackPower
        {
            get => attackPower;
        }

        [SerializeField]

        private float staggerDuration = 0f;

        public float StaggerDuration
        {
            get => staggerDuration;
        }

        [SerializeField]

        private float knockbackDistance = 0f;

        public float KnockbackDistance
        {
            get => knockbackDistance;
        }

        public override List<string> GetHeaders()
        {
            return new List<string>()
            {
                nameof(name),

                nameof(maxHealth),

                nameof(movementSpeed),

                nameof(attackPower),

                nameof(staggerDuration),

                nameof(knockbackDistance),
            };
        }

        public override void Import(GstuSpreadSheet sheet)
        {
            maxHealth = float.Parse(sheet[name, nameof(maxHealth)].value);

            movementSpeed = float.Parse(sheet[name, nameof(movementSpeed)].value);

            attackPower = float.Parse(sheet[name, nameof(attackPower)].value);

            staggerDuration = float.Parse(sheet[name, nameof(staggerDuration)].value);

            knockbackDistance = float.Parse(sheet[name, nameof(knockbackDistance)].value);
        }

        public override List<string> Export()
        {
            return new List<string>()
            {
                name.ToString(),

                maxHealth.ToString(),

                movementSpeed.ToString(),

                attackPower.ToString(),

                staggerDuration.ToString(),

                knockbackDistance.ToString(),
            };
        }
    }
}