using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    private float _damageMultiplier = 1;
    private float _damage;
    private float _lifeTime;
    private float _speed;
    private Transform _spot;

    private Coroutine _coroutineFly;

    public event Action<Bullet> Destroyed;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        if (_coroutineFly != null)
        {
            StopCoroutine(_coroutineFly);
        }
    }

    public virtual void Init(float lifeTime,float speed, int damage, Transform spot)
    {
        _lifeTime = lifeTime;
        _speed = speed;
        _damage = _damageMultiplier * damage;
        transform.position = spot.position;
        _spot = spot;
        Fly();
    }

    public virtual void Fly()
    {
        _coroutineFly = StartCoroutine(LifeTime());
    }

    private IEnumerator LifeTime()
    {
        float timer = 0f;

        Vector2 direction = _spot.right.normalized;

        while (timer < _lifeTime)
        {
            _rigidbody2D.linearVelocity = direction * _speed;

            timer += Time.deltaTime;

            yield return null;
        }

        Destroyed?.Invoke(this);
    }

    public float GetDamage() => _damage;

    public void Destroy()
    {
        Destroy(gameObject);
    }
}