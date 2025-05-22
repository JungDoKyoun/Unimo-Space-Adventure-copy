using UnityEngine;

using ZL.Unity.Directing;

namespace ZL.Unity.Unimo
{
    public abstract class StageSceneDirector<TStageSceneDirector> : SceneDirector<TStageSceneDirector>

        where TStageSceneDirector : StageSceneDirector<TStageSceneDirector>
    {

    }
}