using System;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public event Action<Projectile> Destroyed;

    protected Lifetime _lifetime;
    protected Rigidbody2D _rb;

    public virtual void Init(WeaponStats stats, Vector3 pos, Vector2 dir)
    {
        transform.position = pos;
        _lifetime = new Lifetime(this, OnDestroyProjectile);
        _lifetime.Start(stats.LifeTime);
    }

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void OnDestroyProjectile()
    {
        _rb.linearVelocity = Vector2.zero;
        GetComponent<Collider2D>().enabled = false;

        Destroyed?.Invoke(this);
    }
}