using UnityEngine;

public class GrenadeSpawner : ProjectileSpawner
{
    [SerializeField] private PoolProjectile _poolGrenades;
    [SerializeField] private PoolProjectile _poolExplosions;
    [SerializeField] private Projectile _grenadePrefab;
    [SerializeField] private Projectile _explosionPrefab;

    private void Awake()
    {
        _poolGrenades.Init(10, 50, _grenadePrefab);
        _poolExplosions.Init(10, 50, _explosionPrefab);
    }

    public override void Spawn(WeaponStats stats, Vector3 position, Vector2 direction)
    {
        Projectile grenade = _poolGrenades.GetInstance();
        grenade.transform.position = position;
        grenade.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        grenade.gameObject.SetActive(true);
        grenade.Init(stats, position, direction);

        grenade.Destroyed += (proj) =>
        {
            _poolGrenades.ReturnInstance(grenade); // Возвращаем гранату в пул
            SpawnExplosion(proj.transform.position); // Спавним взрыв
        };
    }

    private void SpawnExplosion(Vector3 position)
    {
        Projectile explosion = _poolExplosions.GetInstance();
        explosion.transform.position = position;
        explosion.gameObject.SetActive(true);

        Lifetime lifetime = explosion.GetComponent<Lifetime>();

        if (lifetime != null)
        {
            lifetime.Start(2f);
          //  lifetime.GetComponent<Projectile>().Destroyed +=  (proj) => _poolExplosions.ReturnInstance(explosion);
        }
    }
}