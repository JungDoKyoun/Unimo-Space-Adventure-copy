using GoogleSheetsToUnity;

using System.Collections.Generic;

using UnityEngine;

using ZL.CS.Singleton;

using ZL.Unity.IO.GoogleSheet;

namespace ZL.Unity.Unimo
{
    [CreateAssetMenu(menuName = "ZL/Unimo/SO/Stage Reward Data", fileName = "Stage Reward Data 1")]

    public sealed class StageRewardData : ScriptableGoogleSheetData, ISingleton<StageRewardData>
    {
        public static StageRewardData Instance
        {
            get => ISingleton<StageRewardData>.Instance;
        }

        [Space]

        [SerializeField]

        private int inGameMoneyAmountMin = 0;

        public int InGameMoneyAmountMin
        {
            get => inGameMoneyAmountMin;
        }

        [SerializeField]

        private int inGameMoneyAmountMax = 0;

        public int InGameMoneyAmountMax
        {
            get => inGameMoneyAmountMax;
        }

        [Space]

        [SerializeField]

        private int outGameMoneyAmountMin = 0;

        public int OutGameMoneyAmountMin
        {
            get => outGameMoneyAmountMin;
        }

        [SerializeField]

        private int outGameMoneyAmountMax = 0;

        public int OutGameMoneyAmountMax
        {
            get => outGameMoneyAmountMax;
        }

        [Space]

        [SerializeField]

        private int bluePrintCount = 0;

        [Space]

        [SerializeField]

        private float relicChance = 0f;

        public float RelicChance
        {
            get => relicChance;
        }

        [SerializeField]

        private int relicCount = 0;

        public int RelicCount
        {
            get => relicCount;
        }

        public RelicDropTable RelicDropTable
        {
            get => RelicDropTableSheet.Instance[name];
        }

        public int DropedInGameMoneyAmount { get; private set; } = 0;

        public int DropedOutGameMoneyAmount { get; private set; } = 0;

        public int DropedBluePrintCount { get; private set; } = 0;

        public RelicData[] DropedRelicDatas { get; private set; } = null;

        public override List<string> GetHeaders()
        {
            return new List<string>()
            {
                nameof(name),

                nameof(inGameMoneyAmountMin),

                nameof(inGameMoneyAmountMax),

                nameof(outGameMoneyAmountMin),

                nameof(outGameMoneyAmountMax),

                nameof(bluePrintCount),

                nameof(relicChance),

                nameof(relicCount),
            };
        }

        public override void Import(GstuSpreadSheet sheet)
        {
            inGameMoneyAmountMin = int.Parse(sheet[name, nameof(inGameMoneyAmountMin)].value);

            inGameMoneyAmountMax = int.Parse(sheet[name, nameof(inGameMoneyAmountMax)].value);

            outGameMoneyAmountMin = int.Parse(sheet[name, nameof(outGameMoneyAmountMin)].value);

            outGameMoneyAmountMax = int.Parse(sheet[name, nameof(outGameMoneyAmountMax)].value);

            bluePrintCount = int.Parse(sheet[name, nameof(bluePrintCount)].value);

            relicChance = float.Parse(sheet[name, nameof(relicChance)].value);

            relicCount = int.Parse(sheet[name, nameof(relicCount)].value);
        }

        public override List<string> Export()
        {
            return new List<string>()
            {
                name.ToString(),

                inGameMoneyAmountMin.ToString(),

                inGameMoneyAmountMax.ToString(),

                outGameMoneyAmountMin.ToString(),

                outGameMoneyAmountMax.ToString(),

                bluePrintCount.ToString(),

                relicChance.ToString(),

                relicCount.ToString(),
            };
        }

        public void DropRewards()
        {
            DropedInGameMoneyAmount = Random.Range(inGameMoneyAmountMin, inGameMoneyAmountMax);

            DropedOutGameMoneyAmount = Random.Range(outGameMoneyAmountMin, outGameMoneyAmountMax);

            DropedBluePrintCount = bluePrintCount;

            DropedRelicDatas = null;

            if (RandomEx.DrawLots(relicChance) == false)
            {
                Debug.Log("(테스트) 유물 무조건 드랍되게 설정");

                //return;
            }

            DropRelics();
        }

        public void DropRelics()
        {
            DropedRelicDatas = RelicDropTable.DropRelics(relicCount);
        }

        void ISingleton<StageRewardData>.Release()
        {
            DropedInGameMoneyAmount = 0;

            DropedOutGameMoneyAmount = 0;

            DropedBluePrintCount = 0;

            DropedRelicDatas = null;
        }
    }
}