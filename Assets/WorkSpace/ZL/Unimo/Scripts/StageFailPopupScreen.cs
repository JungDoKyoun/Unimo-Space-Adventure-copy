using TMPro;

using UnityEngine;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Stage Fail Popup Screen")]

    public sealed class StageFailPopupScreen : MonoBehaviour
    {
        [Space]

        [SerializeField]

        private TextMeshProUGUI stagePlayTimeText = null;

        [Space]

        [SerializeField]

        private Clock stagePlayTimeClock = null;

        private void OnEnable()
        {
            stagePlayTimeText.text = $"플레이 시간: {stagePlayTimeClock.GetTimeStamp()}";
        }
    }
}