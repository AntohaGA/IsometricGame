using UnityEngine;

public class Grenade : Bullet
{
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private float _explosionRadius = 2f;

    private int _damage = 100;

    private void Start()
    {
        Destroyed += CreateExplosion;
    }
     
    public override void Destroy()
    {
        CreateExplosion(this);
        base.Destroy();
    }

    private void CreateExplosion(Bullet bullet)
    {
        bullet.Destroyed -= CreateExplosion;

        if (_explosionPrefab != null)
        {
            GameObject explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(explosion, 1f);
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _explosionRadius);

        foreach (Collider2D hit in colliders)
        {
            if (hit.TryGetComponent<IDamagable>(out var damagable))
            {
                damagable.TakeDamage(_damage);
            }
        }
    }
}