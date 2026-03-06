using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    [SerializeField] private GameObject _explosionPrefab;

    private float _explosionRadius;
    private int _damage;
    private Bullet _bullet;

    public float ExplosionRadius => _explosionRadius;

    public void Explode(Vector3 position, float explosionRadius, int damage, Bullet bullet)
    {
        _bullet = bullet;

        _explosionRadius = explosionRadius;
        _damage = damage;

        if (_explosionPrefab != null)
        {
            GameObject explosion = Instantiate(_explosionPrefab, position, Quaternion.identity);
            Destroy(explosion, 2f);
        }

        DamageEnemiesInRadius(position);
    }

    private void DamageEnemiesInRadius(Vector3 center)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(center, _explosionRadius);

        foreach (Collider2D hit in colliders)
        {
            if (hit.TryGetComponent<IDamagable>(out var damagable))
            {
                damagable.TakeDamage(_bullet.Damage);
            }
        }

        Debug.Log("Destroy from Explosion");
        _bullet.Destroy();
    }
}