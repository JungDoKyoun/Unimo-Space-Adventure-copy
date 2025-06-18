using TMPro;

using UnityEngine;

using ZL.Unity.UI;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Stage Fail Popup Screen")]

    public sealed class StageFailPopupScreen : ScreenUI
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [Alias("Stage Play Time Text (UI)")]

        private TextMeshProUGUI stagePlayTimeTextUI = null;

        public override void Appear()
        {
            stagePlayTimeTextUI.text = $"플레이 시간: {SceneClock.Instance.GetTimeStamp()}";

            base.Appear();
        }
    }
}