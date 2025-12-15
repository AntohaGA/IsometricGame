using System.Collections;
using UnityEngine;

public class Shotgun : Weapon
{
    [Header("Дробовик настройки")]
    [SerializeField] private int _bulletsCount = 5;
    [SerializeField] private float _spreadAngle = 8f;  // разброс в градусах
    [SerializeField] private float _spreadDistance = 0.1f;  // разброс по позиции спавна

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public Weapon Equip(Vector3 position, Transform parent)
    {
        return Instantiate(this, position, Quaternion.identity, parent);
    }

    public SpriteRenderer GetGunSprite()
    {
        return _spriteRenderer;
    }

    public IEnumerator Reload()
    {
        throw new System.NotImplementedException();
    }

    public void Shoot()
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

            // Создание пули
            Bullet bullet = Instantiate(_bullet, bulletSpawnPos, bulletRotation);

            // Дополнительно можно задать случайную скорость пули
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            if (bulletRb != null)
            {
                float randomSpeedMultiplier = Random.Range(0.9f, 1.1f);
                bulletRb.linearVelocity *= randomSpeedMultiplier;
            }
        }

        StartCoroutine(Effect());
    }

    private IEnumerator Effect()
    {
        GameObject activeShot = Instantiate(_bulletExplosion, _bulletSpawner.position, _bulletSpawner.rotation);

        yield return new WaitForSeconds(1f);

        Destroy(activeShot);
    }

    public void Unequip()
    {
        throw new System.NotImplementedException();
    }
}