using GoogleSheetsToUnity;

using System.Collections.Generic;

using UnityEngine;

using ZL.Unity.IO.GoogleSheet;

namespace ZL.Unity.Unimo
{
    [CreateAssetMenu(menuName = "ZL/Unimo/SO/Gathering Data", fileName = "Gathering Data")]

    public sealed class GatheringData : ScriptableGoogleSheetData
    {
        [Space]

        [SerializeField]

        private float maxHealth = 0f;

        public float MaxHealth
        {
            get => maxHealth;
        }

        [SerializeField]

        private int resourceAmount = 0;

        public int ResourceAmount
        {
            get => resourceAmount;
        }

        public override List<string> GetHeader()
        {
            return new List<string>()
            {
                "name",

                nameof(maxHealth),

                nameof(resourceAmount),
            };
        }

        public override void Import(GstuSpreadSheet sheet)
        {
            maxHealth = float.Parse(sheet[name, nameof(maxHealth)].value);

            resourceAmount = int.Parse(sheet[name, nameof(resourceAmount)].value);
        }

        public override List<string> Export()
        {
            return new List<string>()
            {
                "name",

                maxHealth.ToString(),

                resourceAmount.ToString(),
            };
        }
    }
}