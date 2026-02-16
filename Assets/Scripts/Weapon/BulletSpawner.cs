using UnityEngine;

public abstract class BulletSpawner : MonoBehaviour
{
    private const int PoolCapacity = 10;
    private const int PoolMaxSize = 50;

    protected PoolBullets PoolBullets;

    [SerializeField] protected Bullet PrefabBullet;

    protected virtual void Awake()
    {
        if (PoolBullets == null)
        {
            PoolBullets = gameObject.AddComponent<PoolBullets>();
            PoolBullets.Init(PoolCapacity, PoolMaxSize, PrefabBullet);
        }
    }

    public virtual void Shoot(Transform from, WeaponStats bulletData)
    {
        Bullet bullet = GetBullet(bulletData, from);
    }

    public virtual void ReturnBullet(Bullet bullet)
    {
        bullet.Destroyed -= OnBulletDestroyed;
        PoolBullets.ReturnInstance(bullet);
    }

    public virtual void Reset()
    {
        PoolBullets.ClearPool();
    }

    public void OnBulletDestroyed(Bullet bullet)
    {
        ReturnBullet(bullet);
    }

    private Bullet GetBullet(WeaponStats bulletData, Transform spot)
    {
        Bullet bullet = PoolBullets.GetInstance();
        bullet.Init(bulletData.finalLifeTime, bulletData.finalSpeed, bulletData.finalDamage, spot);
        bullet.Destroyed += OnBulletDestroyed;

        return bullet;
    }
}
