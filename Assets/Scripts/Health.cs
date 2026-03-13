using System;
using System.ComponentModel;
using UnityEngine;

public class Health : MonoBehaviour, IDamagable, IDestroyble
{
    [SerializeField] protected int startHealth = 200;

    [SerializeField, ReadOnly(true)] protected int _currentHealth;

    public event Action OnHit;
    public event Action<int> OnNewHealth;
    public event Action OnDestroy;

    protected virtual void Awake()
    {
        _currentHealth = startHealth;
    }

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
            OnDestroy?.Invoke();
        }
    }
}