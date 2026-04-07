using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Shotgun : Weapon
{
    [SerializeField] private int _pelletCount;
    [SerializeField] private float _coneHalfAngle;

    public override void Shoot()
    {
        for (int i = 0; i < _pelletCount; i++)
        {
            float angleOffset = Random.Range(-_coneHalfAngle, _coneHalfAngle);
            Vector2 pelletDirection = Quaternion.Euler(0, 0, angleOffset) * BulletSpawnerSpot.right;
            ProjectileSpawner.Spawn(WeaponStats, BulletSpawnerSpot.position, pelletDirection);
        }
    }
}