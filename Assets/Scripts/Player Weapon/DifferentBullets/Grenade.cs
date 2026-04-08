using UnityEngine;

public class Grenade : Projectile
{
    // Ссылка на пул взрывов. Можно сделать через менеджер, но для простоты так.
    [SerializeField] private PoolObjects<GrenadeExplosion> _poolExplosions;

    protected override void Awake()
    {
        // Инициализируем общие компоненты (Rigidbody, Collider)
        base.Awake();
    }

    // Метод столкновения. Мы переопределяем логику из базового класса.
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Здесь можно добавить проверку слоя, если нужно
        // Например: if (!IsEnemy(other)) return;

        // Граната не наносит урон через IDamagable.
        // Она просто взрывается при касании.
        Explode();

        // Уничтожаем саму гранату-болванку
        OnDestroyProjectile();
    }

    private void Explode()
    {
        // 1. Получаем взрыв из пула
        GrenadeExplosion explosion = _poolExplosions.GetInstance();

        // 2. Устанавливаем позицию взрыва там, где взорвалась граната
        explosion.transform.position = transform.position;
        explosion.gameObject.SetActive(true);

        // 3. (Опционально) Инициализируем взрыв, если ему нужны статы оружия
        // explosion.Init(WeaponStats, transform.position, Vector2.zero);
    }
}