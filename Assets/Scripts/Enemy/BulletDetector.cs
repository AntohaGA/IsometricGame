using System;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class BulletDetector : MonoBehaviour
{
    public event Action<int> OnBulletDamage;

    private CapsuleCollider2D _capsuleCollider;

    private void Awake()
    {
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    private void OnEnable()
    {
        _capsuleCollider.enabled = enabled;
    }

    private void OnDisable()
    {
        _capsuleCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Bullet bullet))
        {
            Debug.Log("OnTriggerEnter2D " + bullet);
            bullet.OnHitEnemy();
            OnBulletDamage?.Invoke(bullet.Damage);
        }
    }
}