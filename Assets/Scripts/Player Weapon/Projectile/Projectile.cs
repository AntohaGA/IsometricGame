using System;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    protected Collider2D _collider;
    protected Rigidbody2D _rigidbody;
    protected Lifetime _lifetime;

    public event Action<Projectile> Destroyed;

    public virtual void Init(WeaponStats bulletStats, Vector3 pos, Vector2 dir)
    {
        transform.position = pos;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, dir);
        _lifetime = new Lifetime(this, OnDestroyProjectile);
        _lifetime.Start(bulletStats.LifeTime);
    }

    protected virtual void OnDestroyProjectile()
    {
        _rigidbody.linearVelocity = Vector2.zero;
        Destroyed?.Invoke(this);
        gameObject.SetActive(false);
    }
}