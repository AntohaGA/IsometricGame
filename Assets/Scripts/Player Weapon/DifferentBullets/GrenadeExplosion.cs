using UnityEngine;

public class GrenadeExplosion : Projectile
{
    [SerializeField] private float _explosionRadius = 2f;
    [SerializeField] private int _damage = 500; // Урон можно также брать из WeaponStats

    // Этот метод вызывается из Projectile.Init()
    public override void Init(WeaponStats stats, Vector3 pos, Vector2 dir)
    {
        base.Init(stats, pos, dir);

        // Останавливаем физику, чтобы взрыв не сдвинулся
        if (_rigidbody != null)
        {
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.bodyType = RigidbodyType2D.Static;
        }

        // Наносим урон сразу при появлении
        DealAreaDamage();

        // Объект уничтожит сам себя через время жизни (из Lifetime),
        // которое было запущено в базовом методе Init().
    }

    // Взрыву не нужно реагировать на триггеры
    protected override void HandleCollision(Collider2D other) { }

    private void DealAreaDamage()
    {
        // Находим все коллайдеры в радиусе взрыва
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _explosionRadius);

        foreach (Collider2D hit in colliders)
        {
            IDamagable damagable = hit.GetComponentInParent<IDamagable>();
            if (damagable != null)
            {
                damagable.TakeDamage(_damage);
                Debug.Log($"Взрыв нанес {_damage} урона объекту {hit.name}");
            }
        }
    }
}