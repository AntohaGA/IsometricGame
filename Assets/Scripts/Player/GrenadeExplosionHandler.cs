using UnityEngine;

public class GrenadeExplosionHandler : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private float explosionRadius = 2f;
    [SerializeField] private LayerMask explosionMask = -1;  // Какие слои бьёт

    private void OnEnable()
    {
        BulletSpawner spawner = GetComponent<BulletSpawner>();

        if (spawner != null)
        {
            // Подписка через рефлексию или интерфейс (см. ниже)
            SubscribeToGrenadeEvents(spawner);
        }
    }

    private void SubscribeToGrenadeEvents(BulletSpawner spawner)
    {
        // Вариант 1: Через WeaponCollector или глобальный менеджер
        // Вариант 2: Прямое событие (см. BulletSpawner ниже)
    }

    public void OnGrenadeDestroyed(Vector3 position)
    {
        CreateExplosion(position);
    }

    private void CreateExplosion(Vector3 position)
    {
        if (explosionPrefab != null)
        {
            GameObject explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
            Destroy(explosion, 1f);
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, explosionRadius, explosionMask);

        foreach (Collider2D hit in colliders)
        {
            if (hit.TryGetComponent<IDamagable>(out var damagable))
            {
                float distance = Vector2.Distance(position, hit.transform.position);
              //  float falloffDamage = Damage * (1f - distance / explosionRadius);
             //   damagable.TakeDamage(falloffDamage);
            }
        }
    }
}