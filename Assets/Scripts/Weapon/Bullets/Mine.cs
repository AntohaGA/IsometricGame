using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Mine : Bullet
{
    [Header("Mine Settings")]
    [SerializeField] private float _activationDelay = 2f;
    [SerializeField] private ExplosionEffect _explosionEffect;

    private float _explosionRadius = 2f;
    private bool _isActive;
    private CircleCollider2D _detectionCollider;

    protected override void Awake()
    {
        base.Awake();
        _detectionCollider = GetComponent<CircleCollider2D>();
    }

    public override void Init(WeaponStats weaponStats, Vector3 spawnPosition, Vector2 shootDirection, bool isMove)
    {
        _detectionCollider.enabled = false;
        Damage = weaponStats.Damage;
        transform.position = spawnPosition;
        _rigidbody2D.bodyType = RigidbodyType2D.Static;
        _isActive = false;

        StartCoroutine(ActivationSequence());
    }

    private IEnumerator ActivationSequence()
    {
        yield return new WaitForSeconds(_activationDelay);

        _isActive = true;
        _detectionCollider.enabled = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!_isActive) return;

        if (collision.TryGetComponent<IDamagable>(out var component))
        {
            _explosionEffect.Explode(transform.position, _explosionRadius, Damage, this);
        }
    }
}