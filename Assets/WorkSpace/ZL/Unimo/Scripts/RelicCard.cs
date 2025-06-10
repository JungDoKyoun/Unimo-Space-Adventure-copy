using System;

using TMPro;

using UnityEngine;

using UnityEngine.UI;

using ZL.Unity.Pooling;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Relic Card")]

    public sealed class RelicCard : PooledObject
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [Essential]

        private Toggle toggle = null;

        public Toggle Toggle
        {
            get => toggle;
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

        [Space]

        [SerializeField]

        private RelicData relicData = null;

        public event Action<RelicCard> OnSelectedAction = null;

        public event Action<RelicCard> OnDeselectedAction = null;

        private void OnEnable()
        {
            rarityHightlightImageUI.color = relicData.Rarity.GetColor();

            relicImageUI.sprite = relicImageTable[relicData.name];

            relicNameStringTable = relicStringTableSheet[relicData.name + " Name"];

            relicDescriptionStringTable = relicStringTableSheet[relicData.name + " Description"];

            Refresh(StringTable.Language);

            StringTable.OnLanguageChanged += Refresh;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            OnSelectedAction = null;
            
            OnDeselectedAction = null;

            toggle.isOn = false;

            StringTable.OnLanguageChanged -= Refresh;
        }

        public void Initialize(RelicData relicData)
        {
            this.relicData = relicData;
        }

        private void Refresh(StringTableLanguage language)
        {
            relicNameTextUI.text = relicNameStringTable[language];

            relicDescriptionTextUI.text = string.Format(relicDescriptionStringTable[language], relicData.EffectValues);
        }

        public void OnSelect(bool isOn)
        {
            if (isOn == true)
            {
                OnSelectedAction?.Invoke(this);
            }

            else
            {
                OnDeselectedAction?.Invoke(this);
            }
        }
    }
}