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

        private int targetGatheringCount = 0;

        public int TargetGatheringCount
        {
            get => targetGatheringCount;
        }

        [SerializeField]

        private float fuelConsumptionAmount = 0f;

        public float FuelConsumptionAmount
        {
            get => fuelConsumptionAmount;
        }

        public override List<string> GetHeader()
        {
            return new List<string>()
            {
                "name",

                nameof(targetGatheringCount),

                nameof(fuelConsumptionAmount),
            };
        }

        public override void Import(GstuSpreadSheet sheet)
        {
            targetGatheringCount = int.Parse(sheet[name, nameof(targetGatheringCount)].value);

            fuelConsumptionAmount = float.Parse(sheet[name, nameof(fuelConsumptionAmount)].value);
        }

        public override List<string> Export()
        {
            return new List<string>()
            {
                name,

                targetGatheringCount.ToString(),

                fuelConsumptionAmount.ToString(),
            };
        }
    }
}