using JDG;

using System.Collections;

using UnityEngine;

using UnityEngine.Events;

using ZL.CS.Singleton;

using ZL.Unity.Singleton;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Stage Manager (Singleton)")]

    public sealed class StageManager : MonoSingleton<StageManager>
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [ReadOnlyWhenPlayMode]

        private StageDataSheet stageDataSheet = null;

        [SerializeField]

        [UsingCustomProperty]

        [ReadOnlyWhenPlayMode]

        private StageRewardDataSheet stageRewardDataSheet = null;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [ReadOnlyWhenPlayMode]

        private PlayerManager player = null;

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [ReadOnlyWhenPlayMode]

        private GameObject playerUIScreen = null;

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [ReadOnlyWhenPlayMode]

        private GameObject spawners = null;

        [Space]

        [SerializeField]

        private UnityEvent onStageClearEvent = null;

        [Space]

        [SerializeField]

        private UnityEvent onStageFailEvent = null;

        private StageData stageData = null;

        private StageRewardData stageRewardData = null;

        public static int Level { get; set; } = 1;

        protected override void Awake()
        {
            base.Awake();

            stageData = stageDataSheet[Level];

            stageRewardData = stageRewardDataSheet[Level];

            stageDataSheet = null;

            stageRewardDataSheet = null;

            ISingleton<StageData>.TrySetInstance(stageData);

            ISingleton<StageRewardData>.TrySetInstance(stageRewardData);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            ISingleton<StageData>.Release(stageData);

            ISingleton<StageRewardData>.Release(stageRewardData);
        }

        public void StartStage()
        {
            player.OnPlayerDead += StageFail;

            playerUIScreen.SetActive(true);

            spawners.SetActive(true);

            StartCoroutine(ConsumFuelRoutine());
        }

        private IEnumerator ConsumFuelRoutine()
        {
            while (true)
            {
                yield return null;

                PlayerFuelManager.Fuel -= stageData.FuelConsumptionAmount * Time.deltaTime;
            }
        }

        public void StageClear()
        {
            GameStateManager.IsClear = true;

            stageRewardData.DropRewards();

            if (FirebaseDataBaseMgr.Instance != null)
            {
                Debug.Log("(테스트) 스테이지 보상 지급 코루틴 실행");

                StartCoroutine(FirebaseDataBaseMgr.Instance.UpdateRewardIngameCurrency(stageRewardData.DropedInGameMoneyAmount));

                StartCoroutine(FirebaseDataBaseMgr.Instance.UpdateRewardMetaCurrency(stageRewardData.DropedOutGameMoneyAmount));
            }

            onStageClearEvent.Invoke();
        }

        public void StageFail()
        {
            GameStateManager.IsClear = false;

            GameStateManager.IsRestoreMap = false;

            if (FirebaseDataBaseMgr.Instance != null)
            {
                FixedDebug.Log("(테스트) 인 게임 재화 초기화 코루틴 실행");

                StartCoroutine(FirebaseDataBaseMgr.Instance.InitIngameCurrency());
            }

            onStageFailEvent.Invoke();
        }
    }
}