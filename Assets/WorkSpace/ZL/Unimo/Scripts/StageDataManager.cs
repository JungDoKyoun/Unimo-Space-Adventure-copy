using JDG;

using System.Collections;

using UnityEngine;

using UnityEngine.Events;

using ZL.CS.Singleton;

using ZL.Unity.Singleton;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Stage Manager (Singleton)")]

    public sealed class StageDataManager : MonoSingleton<StageDataManager>
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [ReadOnlyWhenPlayMode]

        private StageData stageData = null;

        [SerializeField]

        [UsingCustomProperty]

        [ReadOnlyWhenPlayMode]

        private RewardData rewardData = null;

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

            ISingleton<RewardData>.TrySetInstance(rewardData);

            ISingleton<RelicDropTable>.TrySetInstance(relicDropTable);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            ISingleton<StageData>.Release(stageData);

            ISingleton<RewardData>.Release(rewardData);

            ISingleton<RelicDropTable>.Release(relicDropTable);
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

            rewardData.SetReward();

            if (FirebaseDataBaseMgr.Instance != null)
            {
                StartCoroutine(FirebaseDataBaseMgr.Instance.UpdateRewardIngameCurrency(rewardData.InGameCurrencyAmount));

                StartCoroutine(FirebaseDataBaseMgr.Instance.UpdateRewardMetaCurrency(rewardData.OutGameCurrencyAmount));
            }

            if (rewardData.IsRelicDroped == true)
            {
                int relicDropCount = rewardData.RelicDropCount;

                relicDropTable.Drop(relicDropCount);

                foreach (var relic in relicDropTable.DropedRelics)
                {
                    Debug.Log(relic);
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
                StartCoroutine(FirebaseDataBaseMgr.Instance.InitIngameCurrency());
            }
        }
    }
}