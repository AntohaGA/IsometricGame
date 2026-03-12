using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamagable
{
    [SerializeField] private int startHealth = 200;

    private int _currentHealth;

    public event Action OnHit;
    public event Action OnDead;

    public event Action<int> OnHealthChanged;

    private void Awake()
    {
        _currentHealth = startHealth;
    }

    private void OnEnable()
    {
        _currentHealth = startHealth;
    }

    public void TakeDamage(int amount)
    {
        OnHit?.Invoke();
        OnHealthChanged?.Invoke(amount);
        _currentHealth -= amount;

        if (_currentHealth <= 0)
        {
            OnDead?.Invoke();
        }
    }
}