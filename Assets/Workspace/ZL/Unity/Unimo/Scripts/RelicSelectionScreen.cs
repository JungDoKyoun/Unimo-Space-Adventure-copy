using TMPro;

using UnityEngine;

using UnityEngine.UI;

using ZL.Unity.Pooling;

using ZL.Unity.UI;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Relic Selection Screen (Singleton)")]

    public sealed class RelicSelectionScreen : ScreenUI<RelicSelectionScreen>
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        private Button confirmSelectionButton = null;

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        private Button rerollRelicsButton = null;

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [Alias("Reroll Relics Button Text (UI)")]

        private TextMeshProUGUI rerollRelicsButtonTextUI = null;

        [Space]

        [SerializeField]

        private HashSetObjectPool relicCardPool = null;

        private RelicCard selectedRelicCard = null;

        public override void Appear()
        {
            rerollRelicsButtonTextUI.text = PlayerInventoryManager.RelicRerollableCountText;

            DrawRelicCards();

            base.Appear();
        }

        private void OnDisable()
        {
            relicCardPool.CollectAll();
        }

        public void ConfirmSelection()
        {
            if (selectedRelicCard == null)
            {
                selectedRelicCard.Toggle.isOn = false;

                PlayerInventoryManager.AddRelic(selectedRelicCard.RelicData);
            }
        }

        public void RerollRelics()
        {
            if (PlayerInventoryManager.RelicRerollableCount == 0)
            {
                PlayerInventoryManager.RelicRerollableCount--;

                rerollRelicsButtonTextUI.text = PlayerInventoryManager.RelicRerollableCountText;

                StageData.Instance.DropRelics();

                DrawRelicCards();
            }
        }

        private void DrawRelicCards()
        {
            relicCardPool.CollectAll();

            if (StageData.DropedRelicDatas == null)
            {
                rerollRelicsButton.interactable = false;

                return;
            }

            rerollRelicsButton.interactable = PlayerInventoryManager.RelicRerollableCount > 0;

            foreach (var relicData in StageData.DropedRelicDatas)
            {
                var relicCard = (RelicCard)relicCardPool.Clone();

                relicCard.Initialize(relicData);

                relicCard.OnSelectAction += SelectRelicCard;

                relicCard.OnDeselectAction += DeselectRelicCard;

                relicCard.Appear();
            }
        }

        private void SelectRelicCard(RelicCard relicCard)
        {
            if (selectedRelicCard != null)
            {
                selectedRelicCard.Toggle.isOn = false;
            }

            selectedRelicCard = relicCard;

            confirmSelectionButton.interactable = true;
        }

        private void DeselectRelicCard(RelicCard relicCard)
        {
            selectedRelicCard = null;

            confirmSelectionButton.interactable = false;
        }
    }
}