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

        private GameObject stageClearPopupScreen = null;

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [ReadOnlyWhenPlayMode]

        private GameObject relicSelectionScreen = null;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [ReadOnlyWhenPlayMode]

        private GameObject stageFailPopupScreen = null;

        protected override IEnumerator Start()
        {
            yield return base.Start();

            StageDataManager.Instance.StartStage();
        }

        public void StageClearDirection()
        {
            if (stageClearDirectionRoutine != null)
            {
                return;
            }

            stageClearDirectionRoutine = StageClearDirectionRoutine();

            StartCoroutine(stageClearDirectionRoutine);
        }

        private IEnumerator stageClearDirectionRoutine = null;

        private IEnumerator StageClearDirectionRoutine()
        {
            TimeEx.Pause();

            stageClearPopupScreen.SetActive(true);

            while (stageClearPopupScreen.activeSelf == true)
            {
                yield return null;
            }

            if (RewardData.Instance.IsRelicDroped == true)
            {
                relicSelectionScreen.SetActive(true);

                while (relicSelectionScreen.activeSelf == true)
                {
                    yield return null;
                }
            }

            LoadScene("Station");
        }

        public void StageFailDirection()
        {
            if (stageFailDirectionRoutine != null)
            {
                return;
            }

            stageFailDirectionRoutine = StageFailDirectionRoutine();

            StartCoroutine(stageFailDirectionRoutine);
        }

        private IEnumerator stageFailDirectionRoutine = null;

        private IEnumerator StageFailDirectionRoutine()
        {
            TimeEx.Pause();

            stageFailPopupScreen.SetActive(true);

            while (stageFailPopupScreen.activeSelf == true)
            {
                yield return null;
            }

            LoadScene("Station");
        }
    }
}