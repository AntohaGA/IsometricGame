using System.Collections;
using UnityEngine;

public class Grenade : Bullet
{
    [SerializeField] private GameObject _explosion;
    public float explosionRadius = 2f;

    public override void Init(float lifeTime, float speed, float damage, Transform spot)
    {
        base.Init(lifeTime, speed, damage, spot);
        StartCoroutine(Explode(damage, lifeTime));
    }

    private IEnumerator Explode(float damage, float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);

        GameObject explosion = Instantiate(_explosion, transform.position, transform.rotation);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D hit in colliders)
        {
            if (hit.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(damage);
            }
        }

        Destroy(explosion.gameObject, 1);
    }
}