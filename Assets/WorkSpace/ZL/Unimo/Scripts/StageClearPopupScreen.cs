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

        [UsingCustomProperty]

        [Essential]

        private Clock stagePlayTimeClock = null;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        private ForceLayoutRebuilder content = null;

        [Space]

        [SerializeField]

        [Essential]

        [UsingCustomProperty]

        [Alias("Stage Play Time Text (UI)")]

        private TextMeshProUGUI stagePlayTimeTextUI = null;

        [SerializeField]

        [Essential]

        [UsingCustomProperty]

        [Alias("In-Game Money Amount Text (UI)")]

        private TextMeshProUGUI inGameMoneyAmountTextUI = null;

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [Alias("Out-Game Money Amount Text (UI)")]

        private TextMeshProUGUI outGameMoneyAmountTextUI = null;

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [Alias("Blue Print Count Text (UI)")]

        private TextMeshProUGUI bluePrintCountTextUI = null;

        private void OnEnable()
        {
            stagePlayTimeTextUI.text = $"«√∑π¿Ã Ω√∞£: {stagePlayTimeClock.GetTimeStamp()}";

            inGameMoneyAmountTextUI.gameObject.SetActive(false);

            var rewardData = StageRewardData.Instance;

            if (rewardData.DropedInGameMoneyAmount != 0)
            {
                inGameMoneyAmountTextUI.text = $"»πµÊ ¿Œ ∞‘¿” ¿Á»≠: {rewardData.DropedInGameMoneyAmount}";

                inGameMoneyAmountTextUI.gameObject.SetActive(true);
            }

            outGameMoneyAmountTextUI.gameObject.SetActive(false);

            if (rewardData.DropedOutGameMoneyAmount != 0)
            {
                outGameMoneyAmountTextUI.text = $"»πµÊ æ∆øÙ ∞‘¿” ¿Á»≠: {rewardData.DropedOutGameMoneyAmount}";

                outGameMoneyAmountTextUI.gameObject.SetActive(true);
            }

            bluePrintCountTextUI.gameObject.SetActive(false);

            if (rewardData.DropedBluePrintCount != 0)
            {
                bluePrintCountTextUI.text = $"»πµÊ º≥∞Ëµµ: {rewardData.DropedBluePrintCount}";

                bluePrintCountTextUI.gameObject.SetActive(true);
            }

            content.ForceRebuildLayout();
        }
    }
}