using JDG;

using System.Collections;

using UnityEngine;

using UnityEngine.Events;

using ZL.Unity.Directing;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Gather Stage Scene Director")]

    public sealed class GatherStageSceneDirector : SceneDirector<GatherStageSceneDirector>
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [ReadOnlyWhenPlayMode]

        private GatherStageData stageData = null;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [ReadOnlyWhenPlayMode]

        private PlayerManager player = null;
        
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [ReadOnlyWhenPlayMode]

        private RandomSpawner monster1Spawner1 = null;

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [ReadOnlyWhenPlayMode]

        private RandomSpawner monster2Spawner1 = null;

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [ReadOnlyWhenPlayMode]

        private RandomSpawner gatheringSpawner1 = null;

        [Space]

        [SerializeField]

        private UnityEvent onStageClearEvent = null;

        [Space]

        [SerializeField]

        private UnityEvent onStageFailEvent = null;

        public Transform EnemyTarget { get; private set; } = null;

        private int collectedResourceAmount = 0;

        protected override void Awake()
        {
            base.Awake();

            player.OnPlayerDead += StageFail;

            EnemyTarget = player.transform;
        }

        protected override IEnumerator Start()
        {
            yield return base.Start();

            monster1Spawner1.SetActive(true);

            monster2Spawner1.SetActive(true);

            gatheringSpawner1.SetActive(true);
        }

        public void GetResource(int value)
        {
            collectedResourceAmount += value;

            // UI 업데이트
            FixedDebug.Log($"자원 채집: {collectedResourceAmount}/{stageData.TargetResourceAmount}");

            if (collectedResourceAmount >= stageData.TargetResourceAmount)
            {
                StageClear();
            }
        }

        public void StageClear()
        {
            TimeEx.Pause();

            GameStateManager.IsClear = true;

            if (FirebaseDataBaseMgr.Instance != null)
            {
                StartCoroutine(FirebaseDataBaseMgr.Instance.UpdateRewardIngameCurrency(stageData.RewardIngameCurrency));

                StartCoroutine(FirebaseDataBaseMgr.Instance.UpdateRewardMetaCurrency(stageData.RewardMetaCurrency));
            }

            onStageClearEvent.Invoke();
        }

        public void StageFail()
        {
            TimeEx.Pause();

            GameStateManager.IsClear = false;

            GameStateManager.IsRestoreMap = false;

            if (FirebaseDataBaseMgr.Instance != null)
            {
                StartCoroutine(FirebaseDataBaseMgr.Instance.InitIngameCurrency());
            }

            onStageFailEvent.Invoke();
        }
    }
}