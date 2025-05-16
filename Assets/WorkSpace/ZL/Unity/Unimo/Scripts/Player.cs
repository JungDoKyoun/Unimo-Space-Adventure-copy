using UnityEngine;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Player")]

    public sealed class Player : MonoBehaviour, IDamageable
    {
        private int currentHealth = 10;

        public int CurrentHealth
        {
            get => currentHealth;
        }

        private void OnEnable()
        {
            MonsterManager.Instance.Target = transform;
        }

        private void OnDisable()
        {
            MonsterManager.Instance.Target = null;
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                currentHealth = 0;

                Dead();
            }
        }

        public void Dead()
        {
            //gameObject.SetActive(false);
        }
    }
}