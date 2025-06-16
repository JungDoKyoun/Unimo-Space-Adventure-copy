using GoogleSheetsToUnity;

using System.Collections.Generic;

using System.Linq;

using UnityEngine;

using ZL.CS.Collections;

using ZL.Unity.IO.GoogleSheet;

namespace ZL.Unity.Unimo
{
    [CreateAssetMenu(menuName = "ZL/Unimo/SO/Relic Data", fileName = "Relic Data 1")]

    public sealed class RelicData : ScriptableGoogleSheetData
    {
        [Space]

        [SerializeField]

        private RelicRarity rarity = RelicRarity.Normal;

        public RelicRarity Rarity
        {
            get => rarity;
        }

        [SerializeField]

        private int price = 0;

        public int Price
        {
            get => price;
        }

        [SerializeField]

        private RelicEffect[] effects = null;

        public RelicEffect[] Effects
        {
            get => effects;
        }

        private object[] effectsArgs = null;

        public object[] EffectsArgs
        {
            get
            {
                if (effectsArgs == null)
                {
                    int length = 0;

                    for (int i = 0; i < effects.Length; ++i)
                    {
                        length += Effects[i].Args.Count;
                    }

                    effectsArgs = new object[length];

                    int index = 0;

                    for (int i = 0; i < effects.Length; ++i)
                    {
                        for (int j = 0; j < Effects[i].Args.Count; ++j)
                        {
                            effectsArgs[index++] = Effects[i].Args[j];
                        }
                    }
                }

                return effectsArgs;
            }
        }

        public override List<string> GetHeaders()
        {
            return new List<string>()
            {
                nameof(name),

                nameof(rarity),

                nameof(price),

                nameof(effects),
            };
        }

        public override void Import(GstuSpreadSheet sheet)
        {
            rarity = (RelicRarity)int.Parse(sheet[name, nameof(rarity)].value);

            price = int.Parse(sheet[name, nameof(price)].value);

            effects = ArrayEx.Parse(sheet[name, nameof(effects)].value, '\n', RelicEffect.Parse);
        }

        public override List<string> Export()
        {
            return new List<string>()
            {
                name.ToString(),

                ((int)rarity).ToString(),

                price.ToString(),

                string.Join('\n', effects.Select((relicEffect) => relicEffect.ToString())),
            };
        }
    }
}