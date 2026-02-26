using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] protected BulletStats Stats;
    private BulletStats Final;
    private Rigidbody2D _rigidbody2D;

    public float Damage => Final.Damage;

    private int _currentPenetrations = 0;

    public event Action<Bullet> EndOfLifetime;
    public event Action<Bullet> Destroyed;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        Final = ScriptableObject.CreateInstance<BulletStats>();
    } 

    public virtual void Init(WeaponStats weaponStats, Vector3 spawnPosition, Vector2 shootDirection, bool isMove)
    {
        Final.Damage = weaponStats.Damage * Stats.Damage;
        transform.position = spawnPosition;
        float maxDeviationAngle = isMove ? weaponStats.MoveAccuracy : weaponStats.StopAccuracy;
        float deviationAngle = UnityEngine.Random.Range(-maxDeviationAngle, maxDeviationAngle) * Mathf.Deg2Rad;
        Vector2 finalDirection = Quaternion.Euler(0, 0, deviationAngle) * shootDirection.normalized;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, finalDirection);

        _rigidbody2D.linearVelocity = finalDirection.normalized * weaponStats.Speed;

        _currentPenetrations = 0; // —брос счетчика

        StartCoroutine(LifeTimer(weaponStats.LifeTime));
    }

    public float GetDamage() => Damage * Mathf.Pow(Stats.DamageFalloff, _currentPenetrations);

    public void OnHitEnemy()
    {
        _currentPenetrations++;
        if (_currentPenetrations >= Stats.MaxPenetrations)
            Destroy(); // ”ничтожить после N врагов
    }

    public virtual void Destroy()
    {
        EndOfLifetime?.Invoke(this);
        Destroyed?.Invoke(this);
    }

    private IEnumerator LifeTimer(float delay)
    {
        yield return new WaitForSeconds(delay);

        Destroy();
    }
}