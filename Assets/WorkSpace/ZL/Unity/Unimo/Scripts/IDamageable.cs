namespace ZL.Unity.Unimo
{
    public interface IDamageable
    {
        public int CurrentHealth { get; }

        public void TakeDamage(int damage);

        public void Dead();
    }
}