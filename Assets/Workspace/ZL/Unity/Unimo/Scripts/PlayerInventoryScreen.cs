using UnityEngine;

using ZL.Unity.Pooling;

using ZL.Unity.UI;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Player Inventory Screen (Singleton)")]

    public sealed class PlayerInventoryScreen : ScreenUI<PlayerInventoryScreen>
    {
        [Space]

        [SerializeField]

        private ManagedObjectPool relicCardPool = null;

        private RelicCard selectedRelicCard = null;

        public override void Appear()
        {
            DrawRelicCards();

            base.Appear();
        }

        private void DrawRelicCards()
        {
            relicCardPool.CollectAll();

            selectedRelicCard = null;

            foreach (var relicData in PlayerInventoryManager.RelicDatas)
            {
                var relicCard = (RelicCard)relicCardPool.Clone();

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

            //confirmSelectionButton.interactable = true;
        }

        private void DeselectRelicCard(RelicCard relicCard)
        {
            selectedRelicCard = null;

            //confirmSelectionButton.interactable = false;
        }
    }
}