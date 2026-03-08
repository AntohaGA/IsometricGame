using UnityEngine;

public class GrenadeExplosion
{
    private readonly Bullet _grenade;
    private readonly GameObject _explosionPrefab;
    private readonly float _explosionRadius = 2;
    private bool _hasExploded = false;



    public GrenadeExplosion(Bullet grenade, GameObject explosionPrefab, float explosionRadius)
    {
        _grenade = grenade;
        _explosionPrefab = explosionPrefab;
        _explosionRadius = explosionRadius;
    }

    public void Explode()
    {
        if (_hasExploded)
            return;

        _hasExploded = true;
        DealAreaDamage();
        SpawnExplosionEffect();
        Object.Destroy(_grenade.gameObject, 1f);
    }

    private void DealAreaDamage()
    {
        var colliders = Physics2D.OverlapCircleAll(_grenade.transform.position, _explosionRadius);

        foreach (var hit in colliders)
        {
            if (hit.TryGetComponent<IDamagable>(out var damagable))
            {
                damagable.TakeDamage(_grenade.Damage);
            }
        }
    }

    private void SpawnExplosionEffect()
    {
        if (_explosionPrefab != null)
        {
            Vector3 spawnPosition = _grenade.transform.position;
            Object.Instantiate(_explosionPrefab, spawnPosition, Quaternion.identity, _grenade.transform);
        }
    }
}