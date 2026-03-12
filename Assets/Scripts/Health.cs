using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamagable
{
    [SerializeField] private int startHealth = 200;

    private int currentHealth;

    public event Action OnHit;
    public event Action OnDead;

    private void Awake()
    {
        currentHealth = startHealth;
    }

    public void TakeDamage(int amount)
    {
        OnHit?.Invoke();
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            OnDead?.Invoke();
        }
    }
}