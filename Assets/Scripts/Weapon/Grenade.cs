using System.Collections;
using UnityEngine;

public class Grenade : Bullet
{
    [SerializeField] private GameObject _explosion;
    public float explosionRadius = 2f;

    public override void Init(float lifeTime, float speed, int damage, Transform spot)
    {
        base.Init(lifeTime, speed, damage, spot);
        StartCoroutine( Explode(damage, lifeTime));       
    }

    private IEnumerator Explode(int damage,float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);

        GameObject explosion = Instantiate(_explosion, transform.position, transform.rotation);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D hit in colliders)
        {
            if (hit.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(damage);
                Debug.Log("кого-то нашли для взрыва");
            }
        }

        Debug.Log("Ещё не удалена пуля");
        Destroy(explosion.gameObject, 1);
    }
}