using UnityEngine;

using ZL.Unity.Directing;

namespace ZL.Unity.Unimo
{
    public abstract class StageSceneDirector : SceneDirector<StageSceneDirector>
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [ReadOnlyWhenPlayMode]

        private PlayerManager player = null;

        public Transform EnemyTarget { get; private set; } = null;

        protected override void Awake()
        {
            base.Awake();

            EnemyTarget = player.transform;
        }

        private void OnStageClear()
        {

        }
    }
}