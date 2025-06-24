using JDG;

using System.Collections;

using UnityEngine;

using ZL.CS.Singleton;

using ZL.Unity.Directing;

using ZL.Unity.UI;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Stage Scene Director (Singleton)")]

    public sealed class StageSceneDirector : SceneDirector<StageSceneDirector>
    {
        #region 스테이지 UI

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

        #endregion

        #region 스테이지 옵션

        [SerializeField]

        [UsingCustomProperty]

        [Line]

        [Text("<b>스테이지 옵션</b>", FontSize = 16)]

        [Margin]

        [Essential]

        [Text("<b>스테이지 데이터</b>")]

        private StageData stageData = null;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Text("<b>유물 드랍 테이블</b>")]

        private RelicDropTable relicDropTable = null;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Text("<b>로드할 씬 이름</b>")]

        private string loadSceneName = "Station";

        #endregion

        #region 테스트 옵션

        #if UNITY_EDITOR

        [SerializeField]

        [UsingCustomProperty]

        [Line]

        [Text("<b>테스트 옵션</b>", FontSize = 16)]

        [Margin]

        [Text("<b>플레이어 무적</b>")]

        private bool isPlayerInvincible = false;

        private bool IsPlayerInvincible
        {
            set
            {
                if (PlayerManager.Instance == null)
                {
                    return;
                }

                if (value == true)
                {
                    PlayerManager.Instance.StopPlayerBlink();

                    PlayerManager.Instance.IsOnHit = true;
                }

                else
                {
                    PlayerManager.Instance.IsOnHit = false;
                }
            }
        }

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Text("<b>연료 소모 여부</b>")]

        private bool isConsumFuel = true;

        private bool IsConsumFuel
        {
            set
            {
                if (PlayerFuelManager.Instance == null)
                {
                    return;
                }

                if (value == true)
                {
                    PlayerFuelManager.Instance.StartConsumFuel();
                }

                else
                {
                    PlayerFuelManager.Instance.StopConsumFuel();
                }
            }
        }

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Text("<b>유물 항상 드롭</b>")]

        [PropertyField]

        [Margin]

        [Button(nameof(StageClear))]

        [Button(nameof(StageFail))]

        private bool alwaysDropRelics = false;

        private void OnValidate()
        {
            if (Application.isPlaying == false)
            {
                return;
            }

            IsPlayerInvincible = isPlayerInvincible;

            IsConsumFuel = isConsumFuel;
        }

        #endif

        #endregion

        protected override void Awake()
        {
            base.Awake();

            ISingleton<StageData>.TrySetInstance(stageData);

            ISingleton<RelicDropTable>.TrySetInstance(relicDropTable);

            if (GatheringManager.Instance != null)
            {
                GatheringManager.Instance.OnGatherCompleted += StageClear;
            }

            PlayerManager.Instance.OnPlayerDead += StageFail;

            PlayerFuelManager.Instance.OnFuelEmpty += StageFail;
        }

        protected override IEnumerator Start()
        {
            yield return base.Start();

            playerUIScreen.Appear();

            PlayerFuelManager.Instance.StartConsumFuel();

            if (SpawnSequence.Instance != null)
            {
                SpawnSequence.Instance.gameObject.SetActive(true);
            }

            #if UNITY_EDITOR

            //IsPlayerInvincible = isPlayerInvincible;

            //IsConsumFuel = isConsumFuel;

            #endif
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
                StartCoroutine(FirebaseDataBaseMgr.Instance.UpdateRewardIngameCurrency(StageData.DropedInGameMoneyAmount));

                StartCoroutine(FirebaseDataBaseMgr.Instance.UpdateRewardMetaCurrency(StageData.DropedOutGameMoneyAmount));
            }

            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.CountStageClear(stageData.Score);
            }

            stageClearPopupScreen.Appear();

            while (stageClearPopupScreen.gameObject.activeSelf == true)
            {
                yield return null;
            }

            #if UNITY_EDITOR

            if (StageData.DropedRelicDatas == null && alwaysDropRelics == true)
            {
                stageData.DropRelics();
            }

            #endif

            if (StageData.DropedRelicDatas != null)
            {
                FixedDebug.Log("유물 드랍됨");

                relicSelectionScreen.Appear();

                while (relicSelectionScreen.gameObject.activeSelf == true)
                {
                    yield return null;
                }
            }

            LoadScene(loadSceneName);
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

                ScoreManager.Instance.CalculateTotalScore();
            }

            stageFailPopupScreen.Appear();

            while (stageFailPopupScreen.gameObject.activeSelf == true)
            {
                yield return null;
            }

            LoadScene(loadSceneName);
        }
    }
}