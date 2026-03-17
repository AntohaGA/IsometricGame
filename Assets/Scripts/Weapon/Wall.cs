using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(NavMeshObstacle))]
public class Wall : MonoBehaviour, IDamagable
{
    [SerializeField] private int _maxHealth = 500;

    private int _health;

    public event Action OnHit;
    public event Action<Wall> DestroyThis;

    private void Awake()
    {
        _health = _maxHealth;
    }
    public void TakeDamage(int damage)
    {
        _health -= damage;
        OnHit?.Invoke();

        if (_health <= 0)
        {
            DestroyThis?.Invoke(this);
            Destroy(gameObject);
        }
    }
}