using System;

public interface IDamagable
{
    public event Action OnHit;
    void TakeDamage(int damage);
}