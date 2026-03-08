using System.Collections;
using UnityEngine;

public class Grenade : Bullet
{
    [SerializeField] private GameObject _explosion;
    [SerializeField] private float explosionRadius = 2.5f;

    private IEnumerator ExplodeLifetime()
    {
        Explode();

        yield return new WaitForSeconds(1);  
        
        InvokeDestroyed();
    }

    private void Explode()
    {
        DamageEnemiesInRadius(transform.position);

        if (_explosion != null)
            Instantiate(_explosion, transform.position, Quaternion.identity);
    }

    private void DamageEnemiesInRadius(Vector3 center)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(center, explosionRadius);

        foreach (Collider2D hit in colliders)
        {
            if (hit.TryGetComponent<IDamagable>(out var damagable))
            {
                damagable.TakeDamage(Damage);
            }
        }
    }

    protected override void Destroy()
    {            
        StartCoroutine(ExplodeLifetime());
    }
}