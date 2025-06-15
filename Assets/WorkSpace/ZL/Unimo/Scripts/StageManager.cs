using JDG;

using System.Collections;

using UnityEngine;

using UnityEngine.Events;

using ZL.CS.Singleton;

using ZL.Unity.Singleton;
using ZL.Unity.UI;

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

        private RelicDropTable relicDropTable = null;

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

        private ScreenUI playerUIScreen = null;

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

        protected override void Awake()
        {
            base.Awake();

            ISingleton<StageData>.TrySetInstance(stageData);

            ISingleton<RelicDropTable>.TrySetInstance(relicDropTable);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            ISingleton<StageData>.Release(stageData);

            ISingleton<RelicDropTable>.Release(relicDropTable);
        }

        public void StartStage()
        {
            player.OnPlayerDead += StageFail;

            playerUIScreen.Appear();

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

            stageData.DropRewards();

            if (FirebaseDataBaseMgr.Instance != null)
            {
                Debug.Log("(테스트) 스테이지 보상 지급 코루틴 실행");

                StartCoroutine(FirebaseDataBaseMgr.Instance.UpdateRewardIngameCurrency(StageData.DropedInGameMoneyAmount));

                StartCoroutine(FirebaseDataBaseMgr.Instance.UpdateRewardMetaCurrency(StageData.DropedOutGameMoneyAmount));
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