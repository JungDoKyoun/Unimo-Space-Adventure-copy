using System.Collections;

using UnityEngine;

using JDG;

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

        /*[Space]

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

        private RandomSpawner gatheringSpawner1 = null;*/

        public Transform EnemyTarget { get; private set; } = null;

        private int collectedResourceAmount = 0;

        protected override void Awake()
        {
            base.Awake();

            player.OnPlayerDead += StageFail;

            EnemyTarget = player.transform;
        }

        public void GetGathering(int resourceAmount)
        {
            collectedResourceAmount += resourceAmount;

            FixedDebug.Log($"자원 채집: {collectedResourceAmount}/{stageData.TargetResourceAmount}");

            if (collectedResourceAmount >= stageData.TargetResourceAmount)
            {
                StageClear();
            }
        }

        private void StageClear()
        {
            StartCoroutine(StageClearRoutine());
        }

        private IEnumerator StageClearRoutine()
        {
            // UI 등장

            FixedDebug.Log("스테이지 클리어");

            FadeOut();

            GameStateManager.IsClear = true;

            yield return FirebaseDataBaseMgr.Instance.UpdateRewardIngameCurrency(stageData.RewardIngameCurrency);

            yield return FirebaseDataBaseMgr.Instance.UpdateRewardMetaCurrency(stageData.RewardMetaCurrency);

            FixedSceneManager.LoadScene(this, fadeDuration, "Station");
        }

        private void StageFail()
        {
            StartCoroutine(StageFailRoutine());
        }

        private IEnumerator StageFailRoutine()
        {
            // UI 등장

            FixedDebug.Log("스테이지 실패");

            FadeOut();

            GameStateManager.IsRestoreMap = false;

            GameStateManager.IsClear = false;

            // 사망 시 인게임 재화 초기화
            yield return FirebaseDataBaseMgr.Instance.InitIngameCurrency();

            FixedSceneManager.LoadScene(this, fadeDuration, "Station");
        }
    }
}