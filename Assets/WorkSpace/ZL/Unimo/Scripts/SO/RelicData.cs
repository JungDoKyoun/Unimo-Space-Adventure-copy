using GoogleSheetsToUnity;

using System.Collections.Generic;

using System.Linq;

using UnityEngine;

using UnityEngine.Serialization;

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

        [SerializeField]

        [FormerlySerializedAs("relicEffects")]

        private RelicEffect[] relicEffects = null;

        public RelicEffect[] Effects
        {
            get => relicEffects;
        }

        private RelicEffectType[] effectTypes = null;

        public RelicEffectType[] EffectTypes
        {
            get
            {
                effectTypes ??= Effects.Select((effect) => effect.Type).ToArray();

                return effectTypes;
            }
        }

        private object[] effectValues = null;

        public object[] EffectValues
        {
            get
            {
                effectValues ??= Effects.Select((effect) => (object)effect.Value).ToArray();

                return effectValues;
            }
        }

        public override List<string> GetHeaders()
        {
            return new List<string>()
            {
                nameof(name),

                nameof(rarity),

                nameof(price),

                nameof(relicEffects),
            };
        }

        public override void Import(GstuSpreadSheet sheet)
        {
            rarity = (RelicRarity)int.Parse(sheet[name, nameof(rarity)].value);

            price = int.Parse(sheet[name, nameof(price)].value);

            relicEffects = ArrayEx.Parse(sheet[name, nameof(relicEffects)].value, '\n', RelicEffect.Parse);
        }

        public override List<string> Export()
        {
            return new List<string>()
            {
                name.ToString(),

                ((int)rarity).ToString(),

                price.ToString(),

                string.Join('\n', relicEffects.Select((relicEffect) => relicEffect.ToString())),
            };
        }
    }
}