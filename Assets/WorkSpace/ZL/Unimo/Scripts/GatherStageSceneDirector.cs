using JDG;

using System.Collections;

using UnityEngine;
using UnityEngine.UI;
using ZL.Unity.Directing;
using ZL.Unity.UI;

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

        private SliderValueDisplayer fuelBar = null;

        [SerializeField]

        private float fuelMax = 100f;

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

        private float fuel = 0f;

        private bool isPlaying = true;

        protected override void Awake()
        {
            base.Awake();

            player.OnPlayerDead += StageFail;

            EnemyTarget = player.transform;

            fuelBar.Slider.maxValue = fuelMax;

            fuelBar.Slider.value = fuel = fuelMax;
        }

        private void Update()
        {
            if (isPlaying == false)
            {
                return;
            }

            fuel -= stageData.FuelDrainAmount * Time.deltaTime;

            fuelBar.Slider.value = fuel;

            if (fuel <= 0f)
            {
                fuel = 0f;

                StageFail();
            }
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

        public void GetFuel(float value)
        {
            fuel += value;

            if (fuel > fuelMax)
            {
                fuel = fuelMax;
            }
        }

        private void StageClear()
        {
            StartCoroutine(StageClearRoutine());
        }

        private IEnumerator StageClearRoutine()
        {
            isPlaying = false;

            // UI 등장
            FixedDebug.Log("스테이지 클리어");

            FadeOut();

            GameStateManager.IsClear = true;

            if (FirebaseDataBaseMgr.Instance != null)
            {
                yield return FirebaseDataBaseMgr.Instance.UpdateRewardIngameCurrency(stageData.RewardIngameCurrency);

                yield return FirebaseDataBaseMgr.Instance.UpdateRewardMetaCurrency(stageData.RewardMetaCurrency);
            }

            FixedSceneManager.LoadScene(this, fadeDuration, "Station");
        }

        private void StageFail()
        {
            StartCoroutine(StageFailRoutine());
        }

        private IEnumerator StageFailRoutine()
        {
            isPlaying = false;

            // UI 등장
            FixedDebug.Log("스테이지 실패");

            FadeOut();

            GameStateManager.IsRestoreMap = false;

            GameStateManager.IsClear = false;

            if (FirebaseDataBaseMgr.Instance != null)
            {
                yield return FirebaseDataBaseMgr.Instance.InitIngameCurrency();
            }

            FixedSceneManager.LoadScene(this, fadeDuration, "Station");
        }
    }
}