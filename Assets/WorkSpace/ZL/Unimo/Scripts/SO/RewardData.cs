using GoogleSheetsToUnity;

using System.Collections.Generic;

using UnityEngine;

using ZL.CS.Singleton;

using ZL.Unity.IO.GoogleSheet;

namespace ZL.Unity.Unimo
{
    [CreateAssetMenu(menuName = "ZL/Unimo/SO/Reward Data", fileName = "Reward Data 1")]

    public sealed class RewardData : ScriptableGoogleSheetData, ISingleton<RewardData>
    {
        public static RewardData Instance
        {
            get => ISingleton<RewardData>.Instance;
        }

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

        public int InGameCurrencyAmount { get; private set; } = 0;

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

        public int OutGameCurrencyAmount { get; private set; } = 0;

        [Space]

        [SerializeField]

        private int bluePrintCount = 0;

        public int BluePrintCount
        {
            get => bluePrintCount;
        }

        [Space]

        [SerializeField]

        private int relicDropCount = 0;

        public int RelicDropCount
        {
            get => relicDropCount;
        }

        [SerializeField]

        private float relicDropRate = 0f;

        public float RelicDropRate
        {
            get => relicDropRate;
        }

        public bool IsRelicDroped { get; private set; } = false;

        public override List<string> GetHeaders()
        {
            return new List<string>()
            {
                nameof(name),

                nameof(inGameCurrencyAmountMin),

                nameof(inGameCurrencyAmountMax),

                nameof(outGameCurrencyAmountMin),

                nameof(outGameCurrencyAmountMax),

                nameof(bluePrintCount),

                nameof(relicDropCount),

                nameof(relicDropRate),
            };
        }

        public override void Import(GstuSpreadSheet sheet)
        {
            inGameCurrencyAmountMin = int.Parse(sheet[name, nameof(inGameCurrencyAmountMin)].value);

            inGameCurrencyAmountMax = int.Parse(sheet[name, nameof(inGameCurrencyAmountMax)].value);

            outGameCurrencyAmountMin = int.Parse(sheet[name, nameof(outGameCurrencyAmountMin)].value);

            outGameCurrencyAmountMax = int.Parse(sheet[name, nameof(outGameCurrencyAmountMax)].value);

            bluePrintCount = int.Parse(sheet[name, nameof(bluePrintCount)].value);

            relicDropCount = int.Parse(sheet[name, nameof(relicDropCount)].value);

            relicDropRate = float.Parse(sheet[name, nameof(relicDropRate)].value);
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

                relicDropCount.ToString(),

                relicDropRate.ToString(),
            };
        }

        public void SetReward()
        {
            InGameCurrencyAmount = Random.Range(inGameCurrencyAmountMin, inGameCurrencyAmountMax);

            OutGameCurrencyAmount = Random.Range(outGameCurrencyAmountMin, outGameCurrencyAmountMax);

            IsRelicDroped = false;

            if (relicDropCount == 0)
            {
                return;
            }

            if (RandomEx.DrawLots(relicDropRate) == false)
            {
                return;
            }

            IsRelicDroped = true;
        }
    }
}