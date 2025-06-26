using UnityEngine;

using ZL.Unity.Pooling;

using ZL.Unity.UI;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Enemy UI Screen (Singleton)")]

    public sealed class EnemyUIScreen : ScreenUI<EnemyUIScreen>
    {
        [Space]

        [SerializeField]

        private HashSetObjectPool<NamedEnemyHealthBar> enemyHealthBarPool = null;

        public NamedEnemyHealthBar AppearHealthBar(Enemy targetEnemy)
        {
            var enemyHealthBar = enemyHealthBarPool.Clone();

            enemyHealthBar.Initialize(targetEnemy);

            enemyHealthBar.Appear();

            return enemyHealthBar;
        }

        public void DisappearHealthBar(NamedEnemyHealthBar enemyHealthBar)
        {
            enemyHealthBarPool.Collect(enemyHealthBar);
        }
    }
}