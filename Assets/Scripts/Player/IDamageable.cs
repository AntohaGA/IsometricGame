using System;

public interface IDamagable
{
    public event Action OnHit;
    public event Action OnDead;
    void TakeDamage(int damage);
}