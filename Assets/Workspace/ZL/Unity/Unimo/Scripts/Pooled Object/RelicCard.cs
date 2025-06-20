using System;

using TMPro;

using UnityEngine;

using UnityEngine.UI;

using ZL.Unity.Pooling;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Relic Card (Pooled)")]

    public sealed class RelicCard : PooledObject
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

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

        public RelicData RelicData
        {
            get => relicData;
        }

        public event Action<RelicCard> OnSelectAction = null;

        public event Action<RelicCard> OnDeselectAction = null;

        protected override void OnDisable()
        {
            StringTable.OnLanguageChanged -= Refresh;

            base.OnDisable();
        }

        public void Initialize(RelicData relicData)
        {
            this.relicData = relicData;
        }

        public override void Appear()
        {
            rarityHightlightImageUI.color = relicData.Rarity.GetColor();

            relicImageUI.sprite = relicImageTable[relicData.name];

            relicNameStringTable = relicStringTableSheet[relicData.name + " Name"];

            relicDescriptionStringTable = relicStringTableSheet[relicData.name + " Description"];

            Refresh();

            StringTable.OnLanguageChanged += Refresh;

            base.Appear();
        }

        public override void Disappear()
        {
            base.Disappear();

            toggle.isOn = false;

            OnSelectAction = null;

            OnDeselectAction = null;
        }

        private void Refresh()
        {
            relicNameTextUI.text = relicNameStringTable.Value;

            relicDescriptionTextUI.text = string.Format(relicDescriptionStringTable.Value, relicData.EffectsArgs);
        }

        public void SetSelect(bool value)
        {
            if (value == true)
            {
                OnSelectAction?.Invoke(this);
            }

            else
            {
                OnDeselectAction?.Invoke(this);
            }
        }
    }
}