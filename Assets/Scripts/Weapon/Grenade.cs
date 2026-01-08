using System.Collections;
using UnityEngine;

public class Grenade : Bullet
{
    [SerializeField] private GameObject _explosion;

    public float explosionRadius = 2f;
    public float explosionDamage = 50f;
    public float explosionDelay = 2f;

    private IEnumerator ExplodeAfterDelay()
    {
        yield return new WaitForSeconds(explosionDelay);

        Explode();
    }

    private void Explode()
    {
        StartCoroutine(Effect());
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D hit in colliders)
        {
            if (hit.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(explosionDamage);
                Debug.Log("кого-то нашли для взрыва");
            }
        }
    }

    private IEnumerator Effect()
    {
        GameObject activeShot = Instantiate(_explosion, transform.position, transform.rotation);

        yield return new WaitForSeconds(2f);

        Destroy(activeShot.gameObject);
    }
}