using GoogleSheetsToUnity;

using System.Collections.Generic;

using UnityEngine;

using ZL.Unity.IO.GoogleSheet;

namespace ZL.Unity.Unimo
{
    [CreateAssetMenu(menuName = "ZL/Unimo/SO/Reward Data", fileName = "Reward Data")]

    public sealed class RewardData : ScriptableGoogleSheetData
    {
        [Space]

        [SerializeField]

        private int inGameCurrencyAmountMin = 0;

        public int InGameCurrencyAmountMin
        {
            get => inGameCurrencyAmountMin;
        }

        [SerializeField]

        private int inGameCurrencyAmountMax = 0;

        public int InGameCurrencyAmountMax
        {
            get => inGameCurrencyAmountMax;
        }

        public static int InGameCurrencyAmount { get; private set; } = 0;

        [Space]

        [SerializeField]

        private int outGameCurrencyAmountMin = 0;

        public int OutGameCurrencyAmountMin
        {
            get => outGameCurrencyAmountMin;
        }

        [SerializeField]

        private int outGameCurrencyAmountMax = 0;

        public int OutGameCurrencyAmountMax
        {
            get => outGameCurrencyAmountMax;
        }

        public static int OutGameCurrencyAmount { get; private set; } = 0;

        [Space]

        [SerializeField]

        private int bluePrintCount = 0;

        public static int BluePrintCount { get; private set; } = 0;

        public override List<string> GetHeader()
        {
            return new List<string>()
            {
                "name",

                nameof(inGameCurrencyAmountMin),

                nameof(inGameCurrencyAmountMax),

                nameof(outGameCurrencyAmountMin),

                nameof(outGameCurrencyAmountMax),

                nameof(bluePrintCount),
            };
        }

        public override void Import(GstuSpreadSheet sheet)
        {
            inGameCurrencyAmountMin = int.Parse(sheet[name, nameof(inGameCurrencyAmountMin)].value);

            inGameCurrencyAmountMax = int.Parse(sheet[name, nameof(inGameCurrencyAmountMax)].value);

            outGameCurrencyAmountMin = int.Parse(sheet[name, nameof(outGameCurrencyAmountMin)].value);

            outGameCurrencyAmountMax = int.Parse(sheet[name, nameof(outGameCurrencyAmountMax)].value);

            bluePrintCount = int.Parse(sheet[name, nameof(bluePrintCount)].value);
        }

        public override List<string> Export()
        {
            return new List<string>()
            {
                name,

                inGameCurrencyAmountMin.ToString(),

                inGameCurrencyAmountMax.ToString(),

                outGameCurrencyAmountMin.ToString(),

                outGameCurrencyAmountMax.ToString(),

                bluePrintCount.ToString(),
            };
        }

        public void SetReward()
        {
            InGameCurrencyAmount = Random.Range(inGameCurrencyAmountMin, inGameCurrencyAmountMax);

            OutGameCurrencyAmount = Random.Range(outGameCurrencyAmountMin, outGameCurrencyAmountMax);

            BluePrintCount = bluePrintCount;
        }
    }
}