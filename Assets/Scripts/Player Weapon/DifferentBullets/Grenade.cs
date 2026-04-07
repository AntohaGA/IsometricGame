using UnityEngine;

public class Grenade : Projectile
{
    [SerializeField] private int _damage;
    [SerializeField] private float _explosionRadius;

    private BulletMovement _movement;

    protected  void Awake()
    {
        _movement = new BulletMovement(_rigidbody);
    }

    public override void Init(WeaponStats stats, Vector3 pos, Vector2 dir)
    {
        base.Init(stats, pos, dir);
        _movement.Move(dir, stats.Speed);
        _damage = stats.Damage;
    }

    protected override void OnDestroyProjectile()
    {
        base.OnDestroyProjectile();
        DealAreaDamage();
        gameObject.SetActive(false);
    }

    private void DealAreaDamage()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, _explosionRadius);

        foreach (var hit in colliders)
        {
            IDamagable damagable = hit.GetComponentInParent<IDamagable>();

            if (damagable != null)
            {
                damagable.TakeDamage(_damage);
            }
        }
    }
}