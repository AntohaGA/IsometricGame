using System;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    protected Collider2D _collider;
    protected Rigidbody2D _rigidbody;
    protected Lifetime _lifetime;

    public event Action<Projectile> Destroyed;

    public virtual void Init(WeaponStats stats, Vector3 pos, Vector2 dir)
    {
        transform.position = pos;
        _lifetime = new Lifetime(this, OnDestroyProjectile);
        _lifetime.Start(stats.LifeTime);
    }

    protected virtual void OnDestroyProjectile()
    {
        _rigidbody.linearVelocity = Vector2.zero;

        Debug.Log("OnDestroyProjectile");
        Destroyed?.Invoke(this);
        gameObject.SetActive(false);
    }
}