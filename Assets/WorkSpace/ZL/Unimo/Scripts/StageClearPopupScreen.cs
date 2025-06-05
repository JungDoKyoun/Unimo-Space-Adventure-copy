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

            var rewardData = RewardData.Instance;

            if (rewardData.InGameCurrencyAmount != 0)
            {
                inGameCurrencyAmountText.text = $"ȹ�� �� ���� ��ȭ: {rewardData.InGameCurrencyAmount}";

                inGameCurrencyAmountText.gameObject.SetActive(true);
            }

            outGameCurrencyAmountText.gameObject.SetActive(false);

            if (rewardData.OutGameCurrencyAmount != 0)
            {
                outGameCurrencyAmountText.text = $"ȹ�� �ƿ� ���� ��ȭ: {rewardData.OutGameCurrencyAmount}";

                outGameCurrencyAmountText.gameObject.SetActive(true);
            }

            bluePrintCountText.gameObject.SetActive(false);

            if (rewardData.BluePrintCount != 0)
            {
                bluePrintCountText.text = $"ȹ�� ���赵: {rewardData.BluePrintCount}";

                bluePrintCountText.gameObject.SetActive(true);
            }

            content.ForceRebuildLayout();
        }
    }
}