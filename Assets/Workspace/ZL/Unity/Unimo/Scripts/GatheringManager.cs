using System;

using TMPro;

using UnityEngine;

using ZL.CS;

using ZL.Unity.Singleton;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Gathering Manager (Singleton)")]

    public sealed class GatheringManager : MonoSingleton<GatheringManager>
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [ReadOnlyWhenPlayMode]

        private TextMeshProUGUI gatherProgressTextUI = null;

        private int targetGatheringCount = 0;

        public int TargetGatheringCount
        {
            set => targetGatheringCount = value;
        }

        private int gatheringCount = 0;

        public int GatheringCount
        {
            get => gatheringCount;

            set
            {
                gatheringCount = MathEx.Clamp(value, 0, targetGatheringCount);

                OnGatheringCountChangedAction?.Invoke(gatheringCount);

                gatherProgressTextUI.text = $"목표 자원: {gatheringCount}/{targetGatheringCount}";

                if (gatheringCount >= targetGatheringCount)
                {
                    OnGatherCompletedAction?.Invoke();
                }
            }
        }
        public event Action<int> OnGatheringCountChangedAction = null;

        public event Action OnGatherCompletedAction = null;

        private void Start()
        {
            targetGatheringCount = StageData.Instance.TargetGatheringCount;

            GatheringCount = 0;
        }
    }
}