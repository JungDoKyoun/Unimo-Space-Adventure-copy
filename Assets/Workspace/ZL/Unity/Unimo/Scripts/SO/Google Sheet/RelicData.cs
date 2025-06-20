using GoogleSheetsToUnity;

using System.Collections.Generic;

using System.Linq;

using UnityEngine;
using ZL.CS.Collections;

using ZL.Unity.SO.GoogleSheet;

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

        private int score = 0;

        public int Score
        {
            get => score;
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
                        length += effects[i].Args.Length;
                    }

                    effectsArgs = new object[length];

                    Refresh();

                    //StringTable.OnLanguageChanged += Refresh;
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

                nameof(score),

                nameof(effects),
            };
        }

        public override void Import(GstuSpreadSheet sheet)
        {
            rarity = (RelicRarity)int.Parse(sheet[name, nameof(rarity)].value);

            price = int.Parse(sheet[name, nameof(price)].value);

            score = int.Parse(sheet[name, nameof(score)].value);

            effects = ArrayEx.Parse(sheet[name, nameof(effects)].value, '\n', RelicEffect.Parse);
        }

        public override List<string> Export()
        {
            return new List<string>()
            {
                name.ToString(),

                ((int)rarity).ToString(),

                price.ToString(),

                score.ToString(),

                string.Join('\n', effects.Select((relicEffect) => relicEffect.ToString())),
            };
        }

        public void Refresh()
        {
            int k = 0;

            for (int i = 0; i < effects.Length; ++i)
            {
                effects[i].Refresh();

                EffectsArgs[k] = effects[i].Args[0];

                k += effects[i].Args.Length;
            }
        }
    }
}