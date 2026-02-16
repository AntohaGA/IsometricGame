using UnityEngine;

public class ShotgunBulletSpawner : BulletSpawner, IShootHandler
{
    [SerializeField] private int _pelletCount = 5;
    [SerializeField] private float _coneHalfAngle = 15f;

    public override void Shoot(Transform from, WeaponStats bulletData)
    {
        for (int i = 0; i < _pelletCount; i++)
        {
            float angleOffset = Random.Range(-_coneHalfAngle, _coneHalfAngle);

            // ✅ Создаём временный спавнер с поворотом
            GameObject tempSpawn = new GameObject("TempShotgunSpawn");
            Transform rotatedSpot = tempSpawn.transform;
            rotatedSpot.SetPositionAndRotation(from.position, from.rotation * Quaternion.Euler(0, 0, angleOffset));

            base.Shoot(rotatedSpot, bulletData);

            // ✅ Удаляем временный объект
            DestroyImmediate(tempSpawn);
        }
    }
}