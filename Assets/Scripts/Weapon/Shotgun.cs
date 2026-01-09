using UnityEngine;

public class Shotgun : Weapon
{
    [Header("Дробовик настройки")]
    [SerializeField] private int _bulletsCount;
    [SerializeField] private float _spreadAngle;  // разброс в градусах
    [SerializeField] private float _spreadDistance;  // разброс по позиции спавна

    public override void Shoot()
    {
        Vector3 spawnPosition = _bulletSpawner.position;

        for (int i = 0; i < _bulletsCount; i++)
        {
            // Случайный разброс по позиции
            Vector3 randomOffset = new Vector3(
                Random.Range(-_spreadDistance, _spreadDistance),
                Random.Range(-_spreadDistance, _spreadDistance),
                0f
            );

            Vector3 bulletSpawnPos = spawnPosition + randomOffset;

            // Случайный угол разброса
            float randomAngle = Random.Range(-_spreadAngle, _spreadAngle);
            Quaternion bulletRotation = _bulletSpawner.rotation * Quaternion.Euler(0, 0, randomAngle);

            Bullet bullet = Instantiate(_bulletPrefab, bulletSpawnPos, bulletRotation);
            bullet.Fly(0.3f,15,30);
        }
    }
}