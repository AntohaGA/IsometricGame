using UnityEngine;

public abstract class BulletSpawner : MonoBehaviour
{
    private const int PoolCapacity = 20;
    private const int PoolMaxSize = 100;

    [SerializeField] protected Bullet PrefabBullet;

    private PoolBullets _poolBullets;

    protected virtual void Awake()
    {
        _poolBullets ??= gameObject.AddComponent<PoolBullets>();
        _poolBullets.Init(PoolCapacity, PoolMaxSize, PrefabBullet);
    }

    public virtual void Reset() => _poolBullets?.ClearPool();
    public void OnBulletDestroyed(Bullet bullet) => ReturnBullet(bullet);

    public virtual void SpawnBullet(WeaponStats weaponStats, Transform spotSpawn)
    {
        Bullet bullet = _poolBullets.GetInstance();
        bullet.Init(weaponStats, spotSpawn);
        bullet.Destroyed += OnBulletDestroyed;
    }

    public virtual void ReturnBullet(Bullet bullet)
    {
        bullet.Destroyed -= OnBulletDestroyed;
        _poolBullets.ReturnInstance(bullet);
    }
}