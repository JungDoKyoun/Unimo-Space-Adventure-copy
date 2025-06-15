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

        protected override IEnumerator Start()
        {
            yield return base.Start();

            StageManager.Instance.StartStage();
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

            stageFailPopupScreen.Appear();

            while (stageFailPopupScreen.gameObject.activeSelf == true)
            {
                yield return null;
            }

            LoadScene("Station");
        }
    }
}