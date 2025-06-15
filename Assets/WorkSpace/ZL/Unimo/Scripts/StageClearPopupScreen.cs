using TMPro;

using UnityEngine;

using ZL.Unity.UI;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Stage Clear Popup Screen")]

    public sealed class StageClearPopupScreen : ScreenUI
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        private ForceLayoutRebuilder popupContent = null;

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

        public override void Appear()
        {
            stagePlayTimeTextUI.text = $"�÷��� �ð�: {SceneClock.Instance.GetTimeStamp()}";

            inGameMoneyAmountTextUI.gameObject.SetActive(false);

            if (StageData.DropedInGameMoneyAmount != 0)
            {
                inGameMoneyAmountTextUI.text = $"ȹ�� �� ���� ��ȭ: {StageData.DropedInGameMoneyAmount}";

                inGameMoneyAmountTextUI.gameObject.SetActive(true);
            }

            outGameMoneyAmountTextUI.gameObject.SetActive(false);

            if (StageData.DropedOutGameMoneyAmount != 0)
            {
                outGameMoneyAmountTextUI.text = $"ȹ�� �ƿ� ���� ��ȭ: {StageData.DropedOutGameMoneyAmount}";

                outGameMoneyAmountTextUI.gameObject.SetActive(true);
            }

            bluePrintCountTextUI.gameObject.SetActive(false);

            if (StageData.DropedBluePrintCount != 0)
            {
                bluePrintCountTextUI.text = $"ȹ�� ���赵: {StageData.DropedBluePrintCount}";

                bluePrintCountTextUI.gameObject.SetActive(true);
            }

            popupContent.ForceRebuildLayout();

            base.Appear();
        }
    }
}