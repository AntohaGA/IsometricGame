using UnityEngine;

public class ShotgunBulletSpawner : ProjectileSpawner
{
    [SerializeField] private int _pelletCount;
    [SerializeField] private float _coneHalfAngle;
    [SerializeField] private PoolObjects<Bullet> _poolBullets; // Только пул для пуль

    public override void Spawn(WeaponStats stats, Vector3 position, Vector2 baseDirection)
    {
        if (stats.ProjectileType != ProjectileType.Bullet)
        {
            Debug.LogWarning("ShotgunSpawner пытается спавнить не пулю!", this);
            return;
        }

        for (int i = 0; i < _pelletCount; i++)
        {
            float angleOffset = Random.Range(-_coneHalfAngle, _coneHalfAngle);
            Vector2 pelletDirection = Quaternion.Euler(0, 0, angleOffset) * baseDirection;

            Bullet bullet = _poolBullets.GetInstance();
            bullet.transform.position = position;
            bullet.transform.rotation = Quaternion.LookRotation(Vector3.forward, pelletDirection);
            bullet.gameObject.SetActive(true);
            bullet.Init(stats, position, pelletDirection);

            bullet.Destroyed += (proj) => _poolBullets.ReturnInstance(proj as Bullet);
        }
    }
}