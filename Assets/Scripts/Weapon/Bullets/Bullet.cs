using System;
using System.Data;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour, IAttack
{
    [SerializeField] private BulletStats Stats;

    private BulletConfig _config;
    private BulletMovement _movement;
    private Lifetime _lifetime;
    private BulletDamage _damageSystem;

    public event Action OnDestroy;

    public int Damage => _config?.damage ?? 0;

    public event Action<Bullet> Destroyed;

    public void DestroyBullet() => OnDestroyBullet();

    protected virtual void Awake()
    {
        _movement = new BulletMovement(GetComponent<Rigidbody2D>());
        _damageSystem = new BulletDamage(this);
    }

    public virtual void Init(WeaponStats weaponStats, Vector3 spawnPosition, Vector2 shootDirection)
    {
        _config = new BulletConfig(Stats, weaponStats);
        _lifetime = new Lifetime(this, OnDestroyBullet);
        _damageSystem.Initialize(_config.penetration);
        transform.position = spawnPosition;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, shootDirection);
        _movement.Move(shootDirection, _config.speed);
        _lifetime.Start(_config.lifeTime);
    }

    protected virtual void OnDestroyBullet()
    {
        OnDestroy?.Invoke();
        Destroyed?.Invoke(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamagable damagable = other.GetComponentInParent<IDamagable>();

        if (damagable != null)
        {
            ApplyDamage(damagable);
        }
    }

    public void ApplyDamage(IDamagable target)
    {
        if (_damageSystem.GiveDamage(target))
        {
            OnDestroyBullet(); 
        }
    }
}