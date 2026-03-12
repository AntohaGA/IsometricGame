using UnityEngine;

public class GrenadeExplosion: MonoBehaviour
{
    [SerializeField] private  GameObject _explosionPrefab;
    [SerializeField] private int _damage = 500;
    private readonly float _explosionRadius = 2;

    private void OnEnable()
    {
        SpawnExplosionEffect();
        DealAreaDamage();
    }

    private void DealAreaDamage()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, _explosionRadius);

        foreach (var hit in colliders)
        {
            if (hit.TryGetComponent<IDamagable>(out var damagable))
            {
                damagable.TakeDamage(_damage);
                Debug.Log(_damage + " - grenade Damage");
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