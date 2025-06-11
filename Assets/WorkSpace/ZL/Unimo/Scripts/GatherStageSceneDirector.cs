using JDG;

using System.Collections;

using UnityEngine;

using ZL.Unity.Directing;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Gather Stage Scene Director (Singleton)")]

    public sealed class GatherStageSceneDirector : SceneDirector<GatherStageSceneDirector>
    {
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

        private Clock stagePlayTimeClock = null;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [ReadOnlyWhenPlayMode]

        [PropertyField]

        [ReadOnly(false)]

        [Button(nameof(StageClear))]

        private GameObject stageClearPopupScreen = null;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [ReadOnlyWhenPlayMode]

        [PropertyField]

        [ReadOnly(false)]

        [Button(nameof(StageFail))]

        private GameObject stageFailPopupScreen = null;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [ReadOnlyWhenPlayMode]

        private GameObject relicSelectionScreen = null;

        protected override void Awake()
        {
            base.Awake();

            PlayerManager.OnPlayerDead += StageFail;
        }

        protected override IEnumerator Start()
        {
            yield return base.Start();

            spawners.SetActive(true);

            stagePlayTimeClock.gameObject.SetActive(true);

            PlayerFuelManager.Instance.StartConsumption();
        }

        public void StageClear()
        {
            if (stageClearRoutine != null)
            {
                return;
            }

            stageClearRoutine = StageClearRoutine();

            StartCoroutine(stageClearRoutine);
        }

        private IEnumerator stageClearRoutine = null;

        private IEnumerator StageClearRoutine()
        {
            TimeEx.Pause();

            GameStateManager.IsClear = true;

            RewardData.Instance.SetReward();

            if (FirebaseDataBaseMgr.Instance != null)
            {
                StartCoroutine(FirebaseDataBaseMgr.Instance.UpdateRewardIngameCurrency(RewardData.Instance.InGameCurrencyAmount));

                StartCoroutine(FirebaseDataBaseMgr.Instance.UpdateRewardMetaCurrency(RewardData.Instance.OutGameCurrencyAmount));
            }

            stageClearPopupScreen.SetActive(true);

            while (stageClearPopupScreen.activeSelf == true)
            {
                yield return null;
            }

            if (RewardData.Instance.IsRelicDroped == true)
            {
                int relicDropCount = RewardData.Instance.RelicDropCount;

                RelicDropData.Instance.Drop(relicDropCount);

                foreach (var relic in RelicDropData.Instance.DropedRelics)
                {
                    Debug.Log(relic);
                }

                relicSelectionScreen.SetActive(true);

                while (relicSelectionScreen.activeSelf == true)
                {
                    yield return null;
                }
            }

            LoadScene("Station");
        }

        public void StageFail()
        {
            if (stageFailRoutine != null)
            {
                return;
            }

            stageFailRoutine = StageFailRoutine();

            StartCoroutine(stageFailRoutine);
        }

        private IEnumerator stageFailRoutine = null;

        private IEnumerator StageFailRoutine()
        {
            TimeEx.Pause();

            GameStateManager.IsClear = false;

            GameStateManager.IsRestoreMap = false;

            if (FirebaseDataBaseMgr.Instance != null)
            {
                StartCoroutine(FirebaseDataBaseMgr.Instance.InitIngameCurrency());
            }

            stageFailPopupScreen.SetActive(true);

            while (stageFailPopupScreen.activeSelf == true)
            {
                yield return null;
            }

            LoadScene("Station");
        }
    }
}