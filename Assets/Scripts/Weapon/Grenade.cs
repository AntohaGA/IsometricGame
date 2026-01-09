using System.Collections;
using UnityEngine;

public class Grenade : Bullet
{
    [SerializeField] private GameObject _explosion;

    public float explosionRadius = 2f;

    public override void Fly(float lifeTime, float speed, int damage)
    {
        base.Fly(lifeTime, speed, damage);
        Explode(damage);
    }
    private void Explode(int damage)
    {
        StartCoroutine(Effect());
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D hit in colliders)
        {
            if (hit.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(damage);
                Debug.Log("кого-то нашли для взрыва");
            }
        }
    }

    private IEnumerator Effect()
    {
        yield return new WaitForSeconds(1f);

        GameObject activeShot = Instantiate(_explosion, transform.position, transform.rotation);

        Destroy(activeShot.gameObject, 2);
    }
}