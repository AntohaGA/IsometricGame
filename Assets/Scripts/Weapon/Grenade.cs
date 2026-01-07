using System.Collections;
using UnityEngine;

public class Grenade : Bullet
{
    [SerializeField] private GameObject _explosion;

    public float explosionRadius = 2f;
    public float explosionDamage = 50f;
    public float explosionDelay = 2f;

    private Vector3 _targetPosition;
    private bool isLaunched = false;

    public void Launch(Vector3 target)
    {
        _targetPosition = target;
        isLaunched = true;
        StartCoroutine(ExplodeAfterDelay());
    }

    private void Update()
    {
       /* if (isLaunched)
        {
            float step = _speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, step);

            if (Vector3.Distance(transform.position, _targetPosition) < 0.1f)
            {
                Explode();
            }
        }*/
    }

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