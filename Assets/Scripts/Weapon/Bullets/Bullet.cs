using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] protected BulletMultiplier Stats;

    private int _damage;
    private int _speed;
    private int _lifeTime;
    private int _penetration;
    private int _currentPenetrations = 0;
    private Vector2 _linearVelocity;

    protected Coroutine _lifeTimerCoroutine;
    protected Rigidbody2D _rigidbody2D;

    public event Action<Bullet> Destroyed;

    public void InvokeDestroyed() => Destroyed?.Invoke(this);

    public int Damage
    {
        get => _damage;
    }

    protected virtual void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void InitStats(WeaponStats weaponStats)
    {
        _currentPenetrations = 0;
        _damage = Stats.DamageMultiplier * weaponStats.Damage;
        _speed = Stats.SpeedMultiplier * weaponStats.Speed;
        _lifeTime = Stats.LifeTimeMultiplier * weaponStats.LifeTime;
        _penetration = Stats.PenetrationMultiplier * weaponStats.Penetration;
    }

    public virtual void Init(WeaponStats weaponStats, Vector3 spawnPosition, Vector2 shootDirection)
    {
        InitStats(weaponStats);
        transform.position = spawnPosition;
        _linearVelocity = GetDirection(shootDirection);
    }

    public virtual void Go()
    {
        _rigidbody2D.linearVelocity = _linearVelocity.normalized * _speed;

        if (_lifeTimerCoroutine != null)
            StopCoroutine(_lifeTimerCoroutine);

        _lifeTimerCoroutine = StartCoroutine(LifeTimer());
    }

    protected virtual void Destroy()
    {
        Destroyed?.Invoke(this);
    }

    protected virtual IEnumerator LifeTimer()
    {
        yield return new WaitForSeconds(_lifeTime);

        _lifeTimerCoroutine = null;
        Destroy();
    }

    protected void GiveDamage(IDamagable damagable)
    {
        DecPenetration();
        damagable.TakeDamage(Damage);
    }

    private Vector2 GetDirection(Vector2 shootDirection)
    {
        Vector2 finalDirection = Quaternion.Euler(0, 0, 0) * shootDirection.normalized;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, finalDirection);

        return finalDirection;
    }

    private void DecPenetration()
    {
        Debug.Log("DecPenetration");
        _currentPenetrations++;

        if (_currentPenetrations >= _penetration)
            Destroy();
    }
}