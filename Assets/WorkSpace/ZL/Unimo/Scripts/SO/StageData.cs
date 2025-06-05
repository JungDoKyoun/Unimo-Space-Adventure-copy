using GoogleSheetsToUnity;

using System.Collections.Generic;

using UnityEngine;

using ZL.CS.Singleton;

using ZL.Unity.IO.GoogleSheet;

namespace ZL.Unity.Unimo
{
    [CreateAssetMenu(menuName = "ZL/Unimo/SO/Stage Data", fileName = "Stage Data 1")]

    public sealed class StageData : ScriptableGoogleSheetData, ISingleton<StageData>
    {
        public static StageData Instance
        {
            get => ISingleton<StageData>.Instance;
        }

        [Space]

        [SerializeField]

        private float fuelConsumptionAmount = 0f;

        public float FuelConsumptionAmount
        {
            get => fuelConsumptionAmount;
        }

        [SerializeField]

        private int targetGatheringCount = 0;

        public int TargetGatheringCount
        {
            get => targetGatheringCount;
        }

        public override List<string> GetHeaders()
        {
            return new List<string>()
            {
                nameof(name),

                nameof(fuelConsumptionAmount),
                
                nameof(targetGatheringCount),
            };
        }

        public override void Import(GstuSpreadSheet sheet)
        {
            fuelConsumptionAmount = float.Parse(sheet[name, nameof(fuelConsumptionAmount)].value);

            targetGatheringCount = int.Parse(sheet[name, nameof(targetGatheringCount)].value);
        }

        public override List<string> Export()
        {
            return new List<string>()
            {
                name,

                fuelConsumptionAmount.ToString(),

                targetGatheringCount.ToString(),
            };
        }
    }
}