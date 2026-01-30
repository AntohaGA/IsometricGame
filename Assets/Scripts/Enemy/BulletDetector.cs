using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class BulletDetector : MonoBehaviour
{
    [SerializeField] private ZombieAnimator _zombieAnimator;
    [SerializeField] private ZombieMover _enemyMover;
    [SerializeField] private float _hp = 100f;

    private CapsuleCollider2D _capsuleCollider;

    private void Start()
    {
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Bullet bullet))
        {
            TakeDamage(bullet.Damage);
            DestroyBullet(bullet);
        }
    }

    public void TakeDamage(float damage)
    {
        _zombieAnimator.Hit();
        _hp -= damage;
        Debug.Log("урон получаю");

        if (_hp <= 0)
        {
            _enemyMover.Stop();
            _enemyMover.enabled = false;
            _capsuleCollider.enabled = false;
            _zombieAnimator.Die();
            Die();
        }
    }

    private void DestroyBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    public void Die()
    {
        StartCoroutine(Dissapear());
    }

    private IEnumerator Dissapear()
    {
        yield return new WaitForSeconds(3);

        Destroy(_enemyMover.gameObject);
    }
}