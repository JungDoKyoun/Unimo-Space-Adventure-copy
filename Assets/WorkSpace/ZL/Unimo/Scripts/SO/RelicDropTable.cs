using GoogleSheetsToUnity;

using System.Collections.Generic;

using UnityEngine;

using ZL.CS.Singleton;

using ZL.Unity.IO.GoogleSheet;

using Random = UnityEngine.Random;

namespace ZL.Unity.Unimo
{
    [CreateAssetMenu(menuName = "ZL/Unimo/SO/Relic Drop Table", fileName = "Relic Drop Table 1")]

    public sealed class RelicDropTable : ScriptableGoogleSheetData, ISingleton<RelicDropTable>
    {
        public static RelicDropTable Instance
        {
            get => ISingleton<RelicDropTable>.Instance;
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

        private KeyValuePair<float, RelicRarity>[] table = null;

        public RelicRarity[] DropedRelics { get; private set; } = null;

        public override List<string> GetHeaders()
        {
            return new List<string>()
            {
                nameof(name),

                nameof(relicDropNormal),

                nameof(relicDropRare),

                nameof(relicDropUnique),

                nameof(relicDropEpic),
            };
        }

        public override void Import(GstuSpreadSheet sheet)
        {
            relicDropNormal = float.Parse(sheet[name, nameof(relicDropNormal)].value);

            relicDropRare = float.Parse(sheet[name, nameof(relicDropRare)].value);

            relicDropUnique = float.Parse(sheet[name, nameof(relicDropUnique)].value);

            relicDropEpic = float.Parse(sheet[name, nameof(relicDropEpic)].value);
        }

        public override List<string> Export()
        {
            return new List<string>()
            {
                name,

                relicDropNormal.ToString(),

                relicDropRare.ToString(),

                relicDropUnique.ToString(),

                relicDropEpic.ToString(),
            };
        }

        public void Drop(int count)
        {
            table ??= new KeyValuePair<float, RelicRarity>[]
            {
                new(relicDropNormal, RelicRarity.Normal),
                
                new(relicDropRare, RelicRarity.Rare),
                
                new(relicDropUnique, RelicRarity.Unique),
                
                new(relicDropEpic, RelicRarity.Epic),
            };

            DropedRelics = new RelicRarity[count];

            for (int i = 0; i < count; ++i)
            {
                float cumulative = 0f;

                for (int j = 0; j < table.Length; ++j)
                {
                    cumulative += table[j].Key;

                    if (cumulative > Random.value)
                    {
                        DropedRelics[i] = table[j].Value;

                        break;
                    }
                }
            }
        }
    }
}