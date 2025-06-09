using TMPro;

using UnityEngine;

using UnityEngine.UI;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Relic Card")]

    public sealed class RelicCard : MonoBehaviour
    {
        [Space]

        [SerializeField]

        private RelicData relicData = null;

        public RelicData RelicData
        {
            get => relicData;

            set => relicData = value;
        }

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        private ImageTable relicImageTable = null;

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        private StringTableSheet relicStringTableSheet = null;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [Alias("Rarity Hightlight Image (UI)")]

        private Image rarityHightlightImageUI = null;

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [Alias("Relic Image (UI)")]

        private Image relicImageUI = null;

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [Alias("Relic Name Text (UI)")]

        private TextMeshProUGUI relicNameTextUI = null;

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [Alias("Relic Description Text (UI)")]

        private TextMeshProUGUI relicDescriptionTextUI = null;

        private StringTable relicNameStringTable = null;

        private StringTable relicDescriptionStringTable = null;

        private void OnEnable()
        {
            rarityHightlightImageUI.color = relicData.Rarity.GetColor();

            relicImageUI.sprite = relicImageTable[relicData.name];

            relicNameStringTable = relicStringTableSheet[relicData.name + " Name"];

            relicDescriptionStringTable = relicStringTableSheet[relicData.name + " Description"];

            Refresh(StringTable.Language);

            StringTable.OnLanguageChanged += Refresh;
        }

        private void OnDisable()
        {
            StringTable.OnLanguageChanged -= Refresh;
        }

        private void Refresh(StringTableLanguage language)
        {
            relicNameTextUI.text = relicNameStringTable[language];

            relicDescriptionTextUI.text = string.Format(relicDescriptionStringTable[language], relicData.EffectValues);
        }
    }
}