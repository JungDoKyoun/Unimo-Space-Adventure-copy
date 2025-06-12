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

        private int level = 1;

        public int Level
        {
            get => level;
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

        public int BluePrintCount
        {
            get => bluePrintCount;
        }

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

        [Space]

        [SerializeField]

        private float relicDropNormal = 0f;

        public float RelicDropNormal
        {
            get => relicDropNormal;
        }

        [SerializeField]

        private float relicDropRare = 0f;

        public float RelicDropRare
        {
            get => relicDropRare;
        }

        [SerializeField]

        private float relicDropUnique = 0f;

        public float RelicDropUnique
        {
            get => relicDropUnique;
        }

        [SerializeField]

        private float relicDropEpic = 0f;

        public float RelicDropEpic
        {
            get => relicDropEpic;
        }

        public int DropedInGameMoneyAmount { get; private set; } = 0;

        public int DropedOutGameMoneyAmount { get; private set; } = 0;

        public int DropedBluePrintCount { get; private set; } = 0;

        public RelicData[] DropedRelicDatas { get; private set; } = null;

        private KeyValuePair<RelicRarity, float>[] relicDropTable = null;

        public override List<string> GetHeaders()
        {
            return new List<string>()
            {
                nameof(name),

                nameof(fuelConsumptionAmount),
                
                nameof(targetGatheringCount),

                nameof(inGameMoneyAmountMin),

                nameof(inGameMoneyAmountMax),

                nameof(outGameMoneyAmountMin),

                nameof(outGameMoneyAmountMax),

                nameof(bluePrintCount),

                nameof(relicChance),

                nameof(relicCount),

                nameof(relicDropNormal),

                nameof(relicDropRare),

                nameof(relicDropUnique),

                nameof(relicDropEpic),
            };
        }

        public override void Import(GstuSpreadSheet sheet)
        {
            fuelConsumptionAmount = float.Parse(sheet[name, nameof(fuelConsumptionAmount)].value);

            targetGatheringCount = int.Parse(sheet[name, nameof(targetGatheringCount)].value);

            inGameMoneyAmountMin = int.Parse(sheet[name, nameof(inGameMoneyAmountMin)].value);

            inGameMoneyAmountMax = int.Parse(sheet[name, nameof(inGameMoneyAmountMax)].value);

            outGameMoneyAmountMin = int.Parse(sheet[name, nameof(outGameMoneyAmountMin)].value);

            outGameMoneyAmountMax = int.Parse(sheet[name, nameof(outGameMoneyAmountMax)].value);

            bluePrintCount = int.Parse(sheet[name, nameof(bluePrintCount)].value);

            relicChance = float.Parse(sheet[name, nameof(relicChance)].value);

            relicCount = int.Parse(sheet[name, nameof(relicCount)].value);

            relicDropNormal = float.Parse(sheet[name, nameof(relicDropNormal)].value);

            relicDropRare = float.Parse(sheet[name, nameof(relicDropRare)].value);

            relicDropUnique = float.Parse(sheet[name, nameof(relicDropUnique)].value);

            relicDropEpic = float.Parse(sheet[name, nameof(relicDropEpic)].value);
        }

        public override List<string> Export()
        {
            return new List<string>()
            {
                name.ToString(),

                fuelConsumptionAmount.ToString(),

                targetGatheringCount.ToString(),

                inGameMoneyAmountMin.ToString(),

                inGameMoneyAmountMax.ToString(),

                outGameMoneyAmountMin.ToString(),

                outGameMoneyAmountMax.ToString(),

                bluePrintCount.ToString(),

                relicChance.ToString(),

                relicCount.ToString(),

                relicDropNormal.ToString(),

                relicDropRare.ToString(),

                relicDropUnique.ToString(),

                relicDropEpic.ToString(),
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
            DropedRelicDatas = GetRandomRelics(relicCount);
        }

        public RelicData[] GetRandomRelics(int count)
        {
            if (count == 0)
            {
                return null;
            }

            relicDropTable ??= new KeyValuePair<RelicRarity, float>[]
            {
                new(RelicRarity.Normal, relicDropNormal),

                new(RelicRarity.Rare, relicDropRare),

                new(RelicRarity.Unique, relicDropUnique),

                new(RelicRarity.Epic, relicDropEpic),
            };

            var relics = new RelicData[count];

            for (int i = 0; i < count; ++i)
            {
                float cumulative = 0f;

                for (int j = 0; j < relicDropTable.Length; ++j)
                {
                    cumulative += relicDropTable[j].Value;

                    if (cumulative <= Random.value)
                    {
                        continue;
                    }

                    var relicDatas = RelicDataSheet.Instance.RelicDictionary[relicDropTable[j].Key];

                    relics[i] = RandomEx.Range(relicDatas);

                    break;
                }
            }

            return relics;
        }

        void ISingleton<StageData>.Release()
        {
            DropedInGameMoneyAmount = 0;

            DropedOutGameMoneyAmount = 0;

            DropedBluePrintCount = 0;

            DropedRelicDatas = null;
        }
    }
}