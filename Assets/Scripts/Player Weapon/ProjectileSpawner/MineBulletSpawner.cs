using UnityEngine;

public class MineSpawner : ProjectileSpawner
{
    [SerializeField] private PoolObjects<Mine> _poolMines;
    [SerializeField] private PoolObjects<GrenadeExplosion> _poolExplosions;

    [SerializeField] private float _timeToAutoDestruct = 60f; // 1 минута

    public override void Spawn(WeaponStats stats, Vector3 position, Vector2 direction)
    {
        Mine mine = _poolMines.GetInstance();
        mine.transform.position = position;
        mine.gameObject.SetActive(true);
        mine.Init(stats, position, direction);
        Rigidbody2D rb = mine.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        Lifetime lifetime = mine.GetComponent<Lifetime>();

        if (lifetime != null)
        {
            lifetime.Start(_timeToAutoDestruct);
          //  lifetime.GetComponent<Projectile>().Destroyed += (proj) => HandleMineDestruction(mine.transform.position);
        }

        mine.Destroyed += (proj) => HandleMineDestruction(mine.transform.position);
        mine.Destroyed += (proj) => _poolMines.ReturnInstance(mine);
    }

    private void HandleMineDestruction(Vector3 position)
    {
        SpawnExplosion(position);
    }

    private void SpawnExplosion(Vector3 position)
    {
        GrenadeExplosion explosion = _poolExplosions.GetInstance();
        explosion.transform.position = position;
        explosion.gameObject.SetActive(true);

        Lifetime lifetime = explosion.GetComponent<Lifetime>();

        if (lifetime != null)
        {
            lifetime.Start(2f);

            // Возвращаем взрыв в пул после его анимации
        //    lifetime.GetComponent<Projectile>().Destroyed += (proj) => _poolExplosions.ReturnInstance(explosion);
        }
    }
}