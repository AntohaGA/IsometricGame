using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    private const float RotatorBulletZ = -90f;
    private Rigidbody2D _rigidbody2D;
    private float _damage;
    private float _lifeTime;
    private float _speed;

    public event Action<Bullet> Destroyed;

    public float Damage => _damage;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public virtual void Init(float lifeTime, float speed, float damage, Transform spot)
    {
        _lifeTime = lifeTime;
        _speed = speed;
        _damage = damage;
        transform.SetPositionAndRotation(spot.position, spot.rotation * Quaternion.Euler(0, 0, RotatorBulletZ));
        CancelInvoke();
        Invoke(nameof(Destroy), _lifeTime);
        _rigidbody2D.linearVelocity = spot.right * _speed;
    }

    private void Destroy()
    {
        Destroyed?.Invoke(this);
        gameObject.SetActive(false);
    }

    private void Despawn()
    {
        CancelInvoke(); // защита от повторного вызова
        Destroyed?.Invoke(this);
        gameObject.SetActive(false);
    }
}