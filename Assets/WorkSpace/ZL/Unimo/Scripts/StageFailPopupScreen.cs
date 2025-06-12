using TMPro;

using UnityEngine;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Stage Fail Popup Screen")]

    public sealed class StageFailPopupScreen : MonoBehaviour
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

        [Alias("Stage Play Time Text (UI)")]

        private TextMeshProUGUI stagePlayTimeTextUI = null;

        private void OnEnable()
        {
            stagePlayTimeTextUI.text = $"플레이 시간: {stagePlayTimeClock.GetTimeStamp()}";
        }
    }
}