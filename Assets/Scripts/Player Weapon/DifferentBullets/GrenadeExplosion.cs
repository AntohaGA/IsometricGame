using System.Collections;
using UnityEngine;

public class GrenadeExplosion: MonoBehaviour
{
    [SerializeField] private  GameObject _explosionPrefab;
    [SerializeField] private int _damage = 500;
    [SerializeField] private float _destroyDelay = 2f;

    private readonly float _explosionRadius = 2;

    private GameObject _explosion;

    private void OnEnable()
    {
        StartCoroutine(ExplodeAndDestroy());
    }

    private IEnumerator ExplodeAndDestroy()
    {
        DealAreaDamage();
        SpawnExplosionEffect();

        yield return new WaitForSeconds(_destroyDelay);

        Destroy(_explosion);
        Destroy(gameObject);
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
            _explosion = Instantiate(_explosionPrefab, spawnPosition, Quaternion.identity);
        }
    }
}