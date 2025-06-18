using JDG;

using System.Collections;

using UnityEngine;

using ZL.CS.Singleton;

using ZL.Unity.Directing;

using ZL.Unity.UI;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Stage Scene Director (Singleton)")]

    public class StageSceneDirector : SceneDirector<StageSceneDirector>
    {
        [SerializeField]

        [UsingCustomProperty]

        [Line]

        [Text("<b>스테이지 데이터</b>", FontSize = 16)]

        [Margin]

        [Essential]

        [PropertyField]

        private StageData stageData = null;

        [SerializeField]

        [UsingCustomProperty]

        [PropertyField]

        [Line]

        private RelicDropTable relicDropTable = null;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [ReadOnlyWhenPlayMode]

        private ScreenUI playerUIScreen = null;

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [ReadOnlyWhenPlayMode]

        private RelicSelectionScreen relicSelectionScreen = null;

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [ReadOnlyWhenPlayMode]

        private StageClearPopupScreen stageClearPopupScreen = null;

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [ReadOnlyWhenPlayMode]

        private StageFailPopupScreen stageFailPopupScreen = null;

        protected virtual string LoadSceneName
        {
            get => "Station";
        }

        protected override void Awake()
        {
            base.Awake();

            ISingleton<StageData>.TrySetInstance(stageData);

            ISingleton<RelicDropTable>.TrySetInstance(relicDropTable);
        }

        protected override IEnumerator Start()
        {
            PlayerManager.OnPlayerDead += StageFail;

            yield return base.Start();

            playerUIScreen.Appear();

            SpawnSequence.Instance.gameObject.SetActive(true);

            PlayerFuelManager.Instance?.StartConsumFuel();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            ISingleton<StageData>.Release(stageData);

            ISingleton<RelicDropTable>.Release(relicDropTable);
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

            stageData.DropRewards();

            if (FirebaseDataBaseMgr.Instance != null)
            {
                yield return FirebaseDataBaseMgr.Instance.UpdateRewardIngameCurrency(StageData.DropedInGameMoneyAmount);

                yield return FirebaseDataBaseMgr.Instance.UpdateRewardMetaCurrency(StageData.DropedOutGameMoneyAmount);
            }

            stageClearPopupScreen.Appear();

            while (stageClearPopupScreen.gameObject.activeSelf == true)
            {
                yield return null;
            }

            if (StageData.DropedRelicDatas != null)
            {
                relicSelectionScreen.Appear();

                while (relicSelectionScreen.gameObject.activeSelf == true)
                {
                    yield return null;
                }
            }

            LoadScene(LoadSceneName);
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

            stageFailPopupScreen.Appear();

            while (stageFailPopupScreen.gameObject.activeSelf == true)
            {
                yield return null;
            }

            LoadScene(LoadSceneName);
        }
    }
}