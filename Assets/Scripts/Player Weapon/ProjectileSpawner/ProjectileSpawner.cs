using UnityEngine;

public abstract class ProjectileSpawner : MonoBehaviour
{
    private const int PoolCapacity = 20;
    private const int PoolMaxSize = 100;

    [SerializeField] protected Projectile PrefabProjectile;

    private PoolProjectile _poolProjectile;

    protected virtual void Awake()
    {
        _poolProjectile ??= gameObject.AddComponent<PoolProjectile>();
        _poolProjectile.Init(PoolCapacity, PoolMaxSize, PrefabProjectile);
    }

    public virtual void Reset() => _poolProjectile?.ClearPool();

    public virtual void SpawnProjectile(WeaponStats weaponStats, Vector3 spawnPosition, Vector2 shootDirection)
    {
        Projectile projectile = _poolProjectile.GetInstance();
        projectile.Init(weaponStats, spawnPosition, shootDirection);
        projectile.Destroyed += ReturnProjectile;
    }

    public virtual void ReturnProjectile(Projectile projectile)
    {
        Debug.Log("ReturnProjectile");
        projectile.Destroyed -= ReturnProjectile;
        _poolProjectile.ReturnInstance(projectile);
    }
}