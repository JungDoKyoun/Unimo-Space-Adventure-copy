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

        private StageData stageData = null;

        [SerializeField]

        [UsingCustomProperty]

        [ReadOnlyWhenPlayMode]

        private StageRewardData stageRewardData = null;

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

        private GameObject spawners = null;

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [ReadOnlyWhenPlayMode]

        private GameObject stageUIScreen = null;

        [Space]

        [SerializeField]

        private UnityEvent onStageClearEvent = null;

        [Space]

        [SerializeField]

        private UnityEvent onStageFailEvent = null;

        protected override void Awake()
        {
            base.Awake();

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

            spawners.SetActive(true);

            stageUIScreen.SetActive(true);

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

            if (stageRewardData != null)
            {
                stageRewardData.DropRewards();

                if (FirebaseDataBaseMgr.Instance != null)
                {
                    FixedDebug.Log("스테이지 보상 지급 코루틴 실행 (문제 생길 가능성 있음)");

                    StartCoroutine(FirebaseDataBaseMgr.Instance.UpdateRewardIngameCurrency(stageRewardData.DropedInGameMoneyAmount));

                    StartCoroutine(FirebaseDataBaseMgr.Instance.UpdateRewardMetaCurrency(stageRewardData.DropedOutGameMoneyAmount));
                }
            }

            onStageClearEvent.Invoke();
        }

        public void StageFail()
        {
            GameStateManager.IsClear = false;

            GameStateManager.IsRestoreMap = false;

            if (FirebaseDataBaseMgr.Instance != null)
            {
                FixedDebug.Log("인 게임 재화 초기화 코루틴 실행 (문제 생길 가능성 있음)");

                StartCoroutine(FirebaseDataBaseMgr.Instance.InitIngameCurrency());
            }

            onStageFailEvent.Invoke();
        }
    }
}