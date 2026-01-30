using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class Bullet : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    [SerializeField] private float _damageMultiplier = 1;
    [SerializeField] private int _pierceCount = 5;
    [SerializeField] private float _startDeviationDegrees;

    private float _damage;
    private float _lifeTime;
    private float _speed;

    private Transform _spot;
    private Coroutine _coroutineFly;
    private Vector2 _initialDirection;

    public event Action<Bullet> Destroyed;

    public float Damage => _damage;

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

    public virtual void Init(float lifeTime, float speed, float damage, Transform spot)
    {
        _lifeTime = lifeTime;
        _speed = speed;
        _damage = _damageMultiplier * damage;
        _spot = spot;
        transform.position = spot.position;
        transform.rotation = spot.rotation;
        transform.Rotate(0f, 0f, -90f);

        // Рассчитываем начальное направление с рандомным отклонением
        Vector2 baseDirection = spot.right.normalized;
        float deviation = UnityEngine.Random.Range(-_startDeviationDegrees, _startDeviationDegrees);
        float deviationRad = deviation * Mathf.Deg2Rad;
        float angle = Mathf.Atan2(baseDirection.y, baseDirection.x) + deviationRad;
        _initialDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;

        Fly();
    }

    public virtual void Fly()
    {
        _coroutineFly = StartCoroutine(LifeTime());
    }

    private IEnumerator LifeTime()
    {
        float timer = 0f;

        Vector2 direction = _initialDirection;

        while (timer < _lifeTime)
        {
            _rigidbody2D.linearVelocity = direction * _speed;
            timer += Time.deltaTime;

            yield return null;
        }

        Destroyed?.Invoke(this);
    }

    public void OnHit()
    {
        _pierceCount--;

        if( _pierceCount == 0)
        {
            Destroyed?.Invoke(this);
        }
    }
}