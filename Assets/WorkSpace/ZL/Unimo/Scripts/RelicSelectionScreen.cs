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

        [Space]

        [SerializeField]

        private ManagedObjectPool relicCardPool = null;

        private RelicCard selectedRelicCard = null;

        private void OnEnable()
        {
            if (StageRewardData.Instance.DropedRelicDatas == null)
            {
                return;
            }

            DrawRelicCards();
        }

        private void OnDisable()
        {
            relicCardPool.CollectAll();
        }

        public void ConfirmSelection()
        {

        }

        public void RerollRelics()
        {
            StageRewardData.Instance.DropRelics();

            DrawRelicCards();
        }

        private void DrawRelicCards()
        {
            relicCardPool.CollectAll();

            selectedRelicCard = null;

            foreach (var relicData in StageRewardData.Instance.DropedRelicDatas)
            {
                var relicCard = (RelicCard)relicCardPool.Cloning();

                relicCard.Initialize(relicData);

                relicCard.OnSelectedAction += SelectRelicCard;

                relicCard.OnDeselectedAction += DeselectRelicCard;

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