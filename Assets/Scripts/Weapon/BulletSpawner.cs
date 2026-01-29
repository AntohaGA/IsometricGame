using UnityEngine;

public abstract class BulletSpawner : MonoBehaviour
{
    private const int PoolCapacity = 10;
    private const int PoolMaxSize = 50;

    protected PoolBullets PoolBullets;

    [SerializeField] protected Bullet PrefabBullet;

    protected virtual void Awake()
    {
        PoolBullets = gameObject.AddComponent<PoolBullets>();
        PoolBullets.Init(PoolCapacity, PoolMaxSize, PrefabBullet);
    }

    public Bullet GetBullet(float lifeTime, float speed, int damage, Transform spot)
    {
        Bullet bullet = PoolBullets.GetInstance();
        bullet.Init(lifeTime, speed, damage, spot);

        bullet.Destroyed += ReturnBullet;

        return bullet;
    }

    public virtual void ReturnBullet(Bullet bullet)
    {
        bullet.Destroyed -= ReturnBullet;
        PoolBullets.ReturnInstance(bullet);
    }

    public virtual void Reset()
    {
        PoolBullets.ClearPool();
    }
}
