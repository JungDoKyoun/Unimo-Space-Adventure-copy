using TMPro;

using UnityEngine;

using ZL.Unity.Pooling;

using ZL.Unity.UI;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Named Enemy Health Bar (Pooled)")]

    public sealed class NamedEnemyHealthBar : PooledUI
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [ReadOnlyWhenPlayMode]

        [Alias("Enemy Name Text (UI)")]

        private TextMeshProUGUI enemyNameTextUI = null;

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [ReadOnlyWhenPlayMode]

        private SliderValueDisplayer healthBar = null;

        private Enemy targetEnemy = null;

        private StringTable enemyNameStringTable = null;

        private void OnEnable()
        {
            StringTableManager.Instance.OnLanguageChangedAction += RefreshText;
        }

        private void OnDisable()
        {
            StringTableManager.Instance.OnLanguageChangedAction -= RefreshText;
        }

        public void Initialize(Enemy targetEnemy)
        {
            this.targetEnemy = targetEnemy;

            enemyNameStringTable = targetEnemy.EnemyNameTable;
        }

        public override void Appear()
        {
            RefreshText();

            healthBar.SetMaxValue(targetEnemy.EnemyData.MaxHealth);

            healthBar.SetValue(targetEnemy.CurrentHealth);

            targetEnemy.OnHealthChangedAction += healthBar.SetValue;

            base.Appear();
        }

        public override void OnDisappeared()
        {
            targetEnemy.OnHealthChangedAction -= healthBar.SetValue;

            base.OnDisappeared();
        }

        private void RefreshText()
        {
            enemyNameTextUI.text = enemyNameStringTable.Value;
        }
    }
}