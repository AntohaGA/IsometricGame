using UnityEngine;

public class Bullet : Projectile
{
    [SerializeField] private BulletStats Stats;

    private BulletConfig _config;
    private BulletMovement _movement;
    private BulletDamage _damageSystem;

    protected override void Awake()
    {
        base.Awake();
        _rb = GetComponent<Rigidbody2D>();
        _movement = new BulletMovement(_rb);
        _damageSystem = new BulletDamage(_config.damage);
    }

    public override void Init(WeaponStats weaponStats, Vector3 spawnPosition, Vector2 shootDirection)
    {
        base.Init(weaponStats, spawnPosition, shootDirection);
        _config = new BulletConfig(Stats, weaponStats);
        transform.position = spawnPosition;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, shootDirection);
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