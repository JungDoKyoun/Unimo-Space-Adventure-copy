using TMPro;

using UnityEngine;

using UnityEngine.UI;

using ZL.Unity.Pooling;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Relic Selection Screen")]

    public sealed class RelicSelectionScreen : MonoBehaviour
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

        private ManagedObjectPool relicCardPool = null;

        private RelicCard selectedRelicCard = null;

        private void OnEnable()
        {
            rerollRelicsButtonTextUI.text = PlayerInventoryManager.RelicRerollableCountText;

            DrawRelicCards();
        }

        private void OnDisable()
        {
            relicCardPool.CollectAll();
        }

        public void ConfirmSelection()
        {
            if (selectedRelicCard == null)
            {
                return;
            }

            PlayerInventoryManager.AddRelic(selectedRelicCard.RelicData);
        }

        public void RerollRelics()
        {
            if (PlayerInventoryManager.RelicRerollableCount == 0)
            {
                return;
            }

            --PlayerInventoryManager.RelicRerollableCount;

            rerollRelicsButtonTextUI.text = PlayerInventoryManager.RelicRerollableCountText;

            relicCardPool.CollectAll();

            StageRewardData.Instance.DropRelics();

            DrawRelicCards();
        }

        private void DrawRelicCards()
        {
            if (StageRewardData.Instance.DropedRelicDatas == null)
            {
                rerollRelicsButton.interactable = false;

                return;
            }

            rerollRelicsButton.interactable = PlayerInventoryManager.RelicRerollableCount > 0;

            foreach (var relicData in StageRewardData.Instance.DropedRelicDatas)
            {
                var relicCard = (RelicCard)relicCardPool.Cloning();

                relicCard.Initialize(relicData);

                relicCard.OnSelectAction += SelectRelicCard;

                relicCard.OnDeselectAction += DeselectRelicCard;

                relicCard.gameObject.SetActive(true);
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