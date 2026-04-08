using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Bullet : Projectile
{
    [SerializeField] private BulletStats Stats;

    private BulletConfig _config;
    private BulletMovement _movement;
    private BulletDamage _damageSystem;



    protected void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _movement = new BulletMovement(_rigidbody);
    }

    public override void Init(WeaponStats weaponStats, Vector3 spawnPosition, Vector2 shootDirection)
    {
        base.Init(weaponStats, spawnPosition, shootDirection);
        _config = new BulletConfig(Stats, weaponStats);
        _damageSystem = new BulletDamage(_config.damage);
        _movement.Move(shootDirection, _config.speed);
        _damageSystem.Initialize(_config.penetration);
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
            OnDestroyProjectile();
        }
    }
}