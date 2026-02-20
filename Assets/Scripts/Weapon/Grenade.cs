using System.Collections;
using UnityEngine;

public class Grenade : Bullet
{
    [SerializeField] private GameObject _explosion;
    [SerializeField] private float explosionRadius = 2f;

    public override void Init(WeaponStats weaponStats, Transform initSpot)
    {
        base.Init(weaponStats, initSpot);
        StartCoroutine(Explode(weaponStats.Damage, weaponStats.LifeTime));
    }

    private IEnumerator Explode(float damage, float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);

        if (_explosion != null)
        {
            GameObject explosion = Instantiate(_explosion, transform.position, Quaternion.identity);
            Destroy(explosion, 1f);
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D hit in colliders)
        {
            if (hit.TryGetComponent<IDamagable>(out var damagable))
            {
                damagable.TakeDamage(Damage);
            }
        }

        Destroy();  // ✅ Используем метод пула
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}