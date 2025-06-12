using UnityEngine;

using ZL.Unity.Pooling;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Player Inventory Screen")]

    public sealed class PlayerInventoryScreen : MonoBehaviour
    {
        [Space]

        [SerializeField]

        private ManagedObjectPool relicCardPool = null;

        private RelicCard selectedRelicCard = null;

        private void OnEnable()
        {
            DrawRelicCards();
        }

        private void DrawRelicCards()
        {
            relicCardPool.CollectAll();

            selectedRelicCard = null;

            foreach (var relicData in PlayerInventoryManager.RelicDatas)
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

            //confirmSelectionButton.interactable = true;
        }

        private void DeselectRelicCard(RelicCard relicCard)
        {
            selectedRelicCard = null;

            //confirmSelectionButton.interactable = false;
        }
    }
}