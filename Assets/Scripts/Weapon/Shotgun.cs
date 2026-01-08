using System.Collections;
using UnityEngine;

public class Shotgun : Weapon, ILootable
{
    [Header("Дробовик настройки")]
    [SerializeField] private int _bulletsCount = 5;
    [SerializeField] private float _spreadAngle = 7f;  // разброс в градусах
    [SerializeField] private float _spreadDistance = 0.1f;  // разброс по позиции спавна

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public IEnumerator Reload()
    {
        throw new System.NotImplementedException();
    }

    public override void PullTrigger()
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

    public void Take(ILootTaker lootTaker)
    {
        throw new System.NotImplementedException();
    }
}