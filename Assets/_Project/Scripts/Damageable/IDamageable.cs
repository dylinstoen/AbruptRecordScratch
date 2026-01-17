
public interface IDamageable
{
    int Health { get; }
    int MaxHealth { get; }
    void TakeDamage(float amount);
    void Die();
}
