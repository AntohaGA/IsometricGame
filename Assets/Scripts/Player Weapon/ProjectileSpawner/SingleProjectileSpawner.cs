using UnityEngine;

public class SingleProjectileSpawner : ProjectileSpawner
{
    [SerializeField] protected PoolProjectile PoolProjectile;
    [SerializeField] private Projectile _prefubBullet;

    private void Awake()
    {
        PoolProjectile.Init(20, 100, _prefubBullet);
    }

    public override void Spawn(WeaponStats stats, Vector3 position, Vector2 direction)
    {
        Projectile projectile = PoolProjectile.GetInstance();
        projectile.transform.position = position;
        projectile.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        projectile.gameObject.SetActive(true);
        projectile.Init(stats, position, direction);
        projectile.Destroyed += (proj) => PoolProjectile.ReturnInstance(proj as Bullet);
    }
}