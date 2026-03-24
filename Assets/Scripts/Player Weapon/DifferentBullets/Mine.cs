using System.Collections;
using UnityEngine;

public class Mine : Projectile
{
    [SerializeField] private int _damage;
    [SerializeField] private float _explosionRadius;

    private Collider2D _detectionCollider;

    protected void Awake()
    {
        _detectionCollider = GetComponent<Collider2D>();
        _detectionCollider.isTrigger = true; // Это триггер!
        _rigidbody.bodyType = RigidbodyType2D.Static;
    }

    public override void Init(WeaponStats stats, Vector3 pos, Vector2 dir)
    {
        base.Init(stats, pos, dir);
        _damage = stats.Damage;
        _explosionRadius = stats.Penetration; // Используем Penetration как радиус

        StartCoroutine(ActivationDelay());
    }

    private IEnumerator ActivationDelay()
    {
        yield return new WaitForSeconds(1f); // Задержка активации

        _detectionCollider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      /*  if (IsInTargetLayer(collision.gameObject)) // Можно добавить проверку слоя врага
        {
            Explode();
        }*/

    }

    private void Explode()
    {
        DealAreaDamage();
        OnDestroyProjectile(); // Умираем и возвращаемся в пул
    }

    private void DealAreaDamage()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, _explosionRadius);

        foreach (var hit in colliders)
        {
            IDamagable damagable = hit.GetComponentInParent<IDamagable>();

            if (damagable != null)
            {
                damagable.TakeDamage(_damage);
            }
        }
    }
}