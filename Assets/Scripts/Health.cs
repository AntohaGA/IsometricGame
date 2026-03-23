using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamagable
{
    [SerializeField] protected int startHealth = 200;

    protected int _currentHealth;

    public event Action OnHit;
    public event Action<int> OnNewHealth;
    public event Action Destroyd;

    protected virtual void OnEnable()
    {
        _currentHealth = startHealth;
    }

    public void TakeDamage(int amount)
    {
        OnHit?.Invoke();
        _currentHealth -= amount;
        OnNewHealth?.Invoke(_currentHealth);

        if (_currentHealth <= 0)
        {
            Destroyd?.Invoke();
        }
    }
}