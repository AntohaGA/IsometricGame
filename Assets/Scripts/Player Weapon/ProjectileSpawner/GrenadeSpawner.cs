using UnityEngine;

public class GrenadeSpawner : ProjectileSpawner
{
    [SerializeField] private PoolGrenades _poolGrenades;
    [SerializeField] private PoolExplosions _poolExplosions;

    public override void Spawn(WeaponStats stats, Vector3 position, Vector2 direction)
    {
        if (stats.ProjectileType != ProjectileType.Grenade)
        {
            Debug.LogWarning("GrenadeSpawner пытается спавнить не гранату!", this);
            return;
        }

        Grenade grenade = _poolGrenades.GetInstance();
        grenade.transform.position = position;
        grenade.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        grenade.gameObject.SetActive(true);
        grenade.Init(stats, position, direction);

        // Подписываемся на взрыв гранаты
        grenade.Destroyed += (proj) =>
        {
            _poolGrenades.ReturnInstance(grenade); // Возвращаем гранату в пул
            SpawnExplosion(proj.transform.position); // Спавним взрыв
        };
    }

    private void SpawnExplosion(Vector3 position)
    {
        GrenadeExplosion explosion = _poolExplosions.GetInstance();
        explosion.transform.position = position;
        explosion.gameObject.SetActive(true);

        // Запускаем время жизни взрыва и возврат в пул
        Lifetime lifetime = explosion.GetComponent<Lifetime>();
        if (lifetime != null)
        {
            lifetime.Start(2f);
            // Подписываемся на самоуничтожение взрыва
            lifetime.GetComponent<Projectile>().Destroyed +=
                (proj) => _poolExplosions.ReturnInstance(explosion);
        }
    }
}