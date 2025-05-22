using GoogleSheetsToUnity;

using System.Collections.Generic;

using UnityEngine;

using ZL.Unity.IO.GoogleSheet;

namespace ZL.Unity.Unimo
{
    [CreateAssetMenu(menuName = "ZL/Unimo/SO/Gather Stage Data", fileName = "Gather Stage Data")]

    public sealed class GatherStageData : ScriptableGoogleSheetData
    {
        [Space]

        [SerializeField]

        private int targetResourceAmount = 0;

        public int TargetResourceAmount
        {
            get => targetResourceAmount;
        }

        [SerializeField]

        private int rewardIngameCurrency = 0;

        public int RewardIngameCurrency
        {
            get => rewardIngameCurrency;
        }

        [SerializeField]

        private int rewardMetaCurrency = 0;

        public int RewardMetaCurrency
        {
            get => rewardMetaCurrency;
        }

        [SerializeField]

        private float fuelDrainAmount = 0f;

        public float FuelDrainAmount
        {
            get => fuelDrainAmount;
        }

        public override List<string> GetHeader()
        {
            return new List<string>()
            {
                "name",

                nameof(targetResourceAmount),

                nameof(rewardIngameCurrency),

                nameof(rewardMetaCurrency),

                nameof(fuelDrainAmount),
            };
        }

        public override void Import(GstuSpreadSheet sheet)
        {
            targetResourceAmount = int.Parse(sheet[name, nameof(targetResourceAmount)].value);

            rewardIngameCurrency = int.Parse(sheet[name, nameof(rewardIngameCurrency)].value);

            rewardMetaCurrency = int.Parse(sheet[name, nameof(rewardMetaCurrency)].value);

            fuelDrainAmount = float.Parse(sheet[name, nameof(fuelDrainAmount)].value);
        }

        public override List<string> Export()
        {
            return new List<string>()
            {
                name,

                targetResourceAmount.ToString(),

                rewardIngameCurrency.ToString(),

                rewardMetaCurrency.ToString(),

                fuelDrainAmount.ToString(),
            };
        }
    }
}