using System.Collections;

using UnityEngine;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Test Stage Scene Director (Singleton)")]

    public class TestStageSceneDirector : StageSceneDirector
    {
        protected override string LoadSceneName
        {
            get => "Test Stage";
        }

        protected override IEnumerator Start()
        {
            SpawnSequence.Instance.gameObject.SetActive(true);

            yield return base.Start();
        }
    }
}