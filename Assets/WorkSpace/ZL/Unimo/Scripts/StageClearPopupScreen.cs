using TMPro;

using UnityEngine;

using ZL.Unity.UI;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Stage Clear Popup Screen")]

    public sealed class StageClearPopupScreen : MonoBehaviour
    {
        [Space]

        [SerializeField]

        private ForceLayoutRebuilder content = null;

        [Space]

        [SerializeField]

        private TextMeshProUGUI stagePlayTimeText = null;

        [SerializeField]

        private TextMeshProUGUI inGameCurrencyAmountText = null;

        [SerializeField]

        private TextMeshProUGUI outGameCurrencyAmountText = null;

        [SerializeField]

        private TextMeshProUGUI bluePrintCountText = null;

        [Space]

        [SerializeField]

        private Clock stagePlayTimeClock = null;

        private void OnEnable()
        {
            stagePlayTimeText.text = $"�÷��� �ð�: {stagePlayTimeClock.GetTimeStamp()}";

            inGameCurrencyAmountText.gameObject.SetActive(false);

            if (RewardData.InGameCurrencyAmount != 0)
            {
                inGameCurrencyAmountText.text = $"ȹ�� �� ���� ��ȭ: {RewardData.InGameCurrencyAmount}";

                inGameCurrencyAmountText.gameObject.SetActive(true);
            }

            outGameCurrencyAmountText.gameObject.SetActive(false);

            if (RewardData.OutGameCurrencyAmount != 0)
            {
                outGameCurrencyAmountText.text = $"ȹ�� �ƿ� ���� ��ȭ: {RewardData.OutGameCurrencyAmount}";

                outGameCurrencyAmountText.gameObject.SetActive(true);
            }

            bluePrintCountText.gameObject.SetActive(false);

            if (RewardData.BluePrintCount != 0)
            {
                bluePrintCountText.text = $"ȹ�� ���赵: {RewardData.BluePrintCount}";

                bluePrintCountText.gameObject.SetActive(true);
            }

            content.ForceRebuildLayout();
        }
    }
}