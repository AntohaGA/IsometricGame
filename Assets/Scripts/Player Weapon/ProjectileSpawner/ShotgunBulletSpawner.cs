using UnityEngine;

public class ShotgunBulletSpawner : ProjectileSpawner
{
    [SerializeField] private int _pelletCount;
    [SerializeField] private float _coneHalfAngle;

    public override void SpawnProjectile(WeaponStats bulletData, Vector3 spawnPosition, Vector2 baseDirection)
    {
        for (int i = 0; i < _pelletCount; i++)
        {
            float angleOffset = Random.Range(-_coneHalfAngle, _coneHalfAngle);
            Vector2 pelletDirection = Quaternion.Euler(0, 0, angleOffset) * baseDirection;

            base.SpawnProjectile(bulletData, spawnPosition, pelletDirection);
        }
    }
}