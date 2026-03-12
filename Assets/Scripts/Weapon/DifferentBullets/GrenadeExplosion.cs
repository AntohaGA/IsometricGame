using UnityEngine;

public class GrenadeExplosion: Bullet
{
    private readonly Bullet _explosionPrefab;
    private readonly float _explosionRadius = 2;

    public void Explode()
    {
        DealAreaDamage();
        SpawnExplosionEffect();
    }

    private void DealAreaDamage()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, _explosionRadius);

        foreach (var hit in colliders)
        {
            if (hit.TryGetComponent<IDamagable>(out var damagable))
            {
                damagable.TakeDamage(Damage);
                Debug.Log(Damage + " - grenade Damage");
            }
        }
    }

    private void SpawnExplosionEffect()
    {
        if (_explosionPrefab != null)
        {
            Vector3 spawnPosition = transform.position;
            Instantiate(_explosionPrefab, spawnPosition, Quaternion.identity);
        }
    }
}