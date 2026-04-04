using UnityEngine;

public class SingleProjectileSpawner : ProjectileSpawner
{
    [SerializeField] private PoolObjects<Bullet> _poolBullets; // Только пул для пуль

    public override void Spawn(WeaponStats stats, Vector3 position, Vector2 direction)
    {
        // Проверяем, что тип снаряда соответствует логике спавнера
        if (stats.ProjectileType != ProjectileType.Bullet)
        {
            Debug.LogWarning("SingleProjectileSpawner пытается спавнить не пулю!", this);
            return;
        }

        Bullet bullet = _poolBullets.GetInstance();
        bullet.transform.position = position;
        bullet.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        bullet.gameObject.SetActive(true);
        bullet.Init(stats, position, direction);

        bullet.Destroyed += (proj) => _poolBullets.ReturnInstance(proj as Bullet);
    }
}