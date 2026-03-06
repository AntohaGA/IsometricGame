using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] protected BulletStats Stats;

    private BulletStats Final;
    private Coroutine _lifeTimerCoroutine; 

    private int _currentPenetrations = 0;

    protected Rigidbody2D _rigidbody2D;

    public event Action<Bullet> Destroyed;

    public int Damage
    {
        get => Final.Damage;
        protected set => Final.Damage = value;
    }

    protected virtual void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        Final = ScriptableObject.CreateInstance<BulletStats>();
    } 

    public virtual void Init(WeaponStats weaponStats, Vector3 spawnPosition, Vector2 shootDirection, bool isMove)
    {
        Final.Damage = weaponStats.Damage * Stats.Damage;
        _currentPenetrations = 0;
        transform.position = spawnPosition;
        Vector2 finalDirection = GetDirection(weaponStats, shootDirection, isMove);
        _rigidbody2D.linearVelocity = finalDirection.normalized * weaponStats.Speed;

        if (_lifeTimerCoroutine != null)
            StopCoroutine(_lifeTimerCoroutine);

        _lifeTimerCoroutine = StartCoroutine(LifeTimer(weaponStats.LifeTime));
    }

    public void OnHitEnemy()
    {
        Debug.Log("OnHitEnemy");

        _currentPenetrations++;

        if (_currentPenetrations >= Stats.MaxPenetrations)
            Destroy();
    }

    public virtual void Destroy()
    {
        Destroyed?.Invoke(this);
    }

    private IEnumerator LifeTimer(float delay)
    {
        yield return new WaitForSeconds(delay);

        _lifeTimerCoroutine = null;
        Destroy();
    }

    private Vector2 GetDirection(WeaponStats weaponStats, Vector2 shootDirection, bool isMove)
    {
        float maxDeviationAngle = isMove ? weaponStats.MoveAccuracy : weaponStats.StopAccuracy;
        float deviationAngle = UnityEngine.Random.Range(-maxDeviationAngle, maxDeviationAngle) * Mathf.Deg2Rad;
        Vector2 finalDirection = Quaternion.Euler(0, 0, deviationAngle) * shootDirection.normalized;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, finalDirection);

        return finalDirection;
    }
}