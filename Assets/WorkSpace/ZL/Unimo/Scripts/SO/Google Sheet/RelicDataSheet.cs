using System.Collections.Generic;

using UnityEngine;

using ZL.CS;

using ZL.CS.Singleton;

using ZL.Unity.Collections;

using ZL.Unity.IO.GoogleSheet;

namespace ZL.Unity.Unimo
{
    [CreateAssetMenu(menuName = "ZL/Unimo/SO/Relic Data Sheet (Singleton)", fileName = "Relic Data Sheet")]

    public sealed class RelicDataSheet : ScriptableGoogleSheet<RelicData>, ISingleton<RelicDataSheet>
    {
        public static RelicDataSheet Instance
        {
            get => ISingleton<RelicDataSheet>.Instance;
        }

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        private SerializableDictionary<RelicRarity, List<RelicData>> relicDictionary = null;

        public SerializableDictionary<RelicRarity, List<RelicData>> RelicDictionary
        {
            get => relicDictionary;
        }

        public override void SerializeDatas()
        {
            dataDictionary.Clear();

            relicDictionary.Clear();

            foreach (var relicRarity in EnumEx.GetValues<RelicRarity>())
            {
                relicDictionary.Add(relicRarity, new());
            }

            for (int i = 0; i < datas.Length; ++i)
            {
                var data = datas[i];

                dataDictionary.Add(data.name, data);

                relicDictionary[data.Rarity].Add(data);
            }

            FixedEditorUtility.SetDirty(this);
        }
    }
}