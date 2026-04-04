using UnityEngine;

public class MineSpawner : ProjectileSpawner
{
    [SerializeField] private PoolObjects<Mine> _poolMines;
    [SerializeField] private PoolObjects<GrenadeExplosion> _poolExplosions; // Используем взрыв от гранаты

    // Время до автоматического взрыва, если мину никто не активировал
    [SerializeField] private float _timeToAutoDestruct = 60f; // 1 минута

    public override void Spawn(WeaponStats stats, Vector3 position, Vector2 direction)
    {
        // Проверяем тип снаряда
        if (stats.ProjectileType != ProjectileType.Mine)
        {
            Debug.LogWarning("MineSpawner пытается спавнить не мину!", this);
            return;
        }

        // 1. Получаем мину из пула
        Mine mine = _poolMines.GetInstance();
        mine.transform.position = position;
        mine.gameObject.SetActive(true);

        // 2. Инициализируем мину
        mine.Init(stats, position, direction);

        // 3. Останавливаем мину (она не должна двигаться)
        Rigidbody2D rb = mine.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        // 4. Запускаем таймер самоуничтожения (через 1 минуту)
        Lifetime lifetime = mine.GetComponent<Lifetime>();
        if (lifetime != null)
        {
            lifetime.Start(_timeToAutoDestruct);

            // Подписываемся на событие самоуничтожения по таймеру
            lifetime.GetComponent<Projectile>().Destroyed +=
                (proj) => HandleMineDestruction(mine.transform.position);
        }

        // 5. Подписываемся на уничтожение от триггера (если враг наступит)
        mine.Destroyed += (proj) => HandleMineDestruction(mine.transform.position);

        // 6. Возвращаем мину в пул, когда она взорвется
        mine.Destroyed += (proj) => _poolMines.ReturnInstance(mine);
    }

    private void HandleMineDestruction(Vector3 position)
    {
        // Логика взрыва при уничтожении мины (и врагом, и по таймеру)
        SpawnExplosion(position);
    }

    private void SpawnExplosion(Vector3 position)
    {
        GrenadeExplosion explosion = _poolExplosions.GetInstance();
        explosion.transform.position = position;
        explosion.gameObject.SetActive(true);

        // Запускаем время жизни взрыва (например, 2 секунды)
        Lifetime lifetime = explosion.GetComponent<Lifetime>();
        if (lifetime != null)
        {
            lifetime.Start(2f);

            // Возвращаем взрыв в пул после его анимации
            lifetime.GetComponent<Projectile>().Destroyed +=
                (proj) => _poolExplosions.ReturnInstance(explosion);
        }
    }
}