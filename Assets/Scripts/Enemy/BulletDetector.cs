using System;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class BulletDetector : MonoBehaviour
{
    public event Action<Bullet> OnBulletDetect;

    private CapsuleCollider2D _capsuleCollider;

    private void Awake()
    {
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    private void OnEnable()
    {
        _capsuleCollider.enabled = true;
    }

    private void OnDisable()
    {
        _capsuleCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Bullet bullet))
        {
            OnBulletDetect?.Invoke(bullet);
        }
    }
}