using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    private const float RotatorBulletZ = 0;
    private Rigidbody2D _rigidbody2D;
    private float _damageMultiplier = 1;
    private float _lifeTimeMultiplier = 1;
    private float _speedMultiplier = 1;

    public event Action<Bullet> Destroyed;

    public float Damage;

    protected virtual void OnDestroyed() => Destroyed?.Invoke(this);
    private void Awake() => _rigidbody2D = GetComponent<Rigidbody2D>();

    public virtual void Init(WeaponStats weaponStats, Transform initSpot)
    {
        Damage = _damageMultiplier * weaponStats.Damage;
        transform.position = initSpot.position;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, initSpot.right) * Quaternion.Euler(0, 0, RotatorBulletZ);
        CancelInvoke();
        Invoke(nameof(Destroy), _lifeTimeMultiplier * weaponStats.LifeTime);
        _rigidbody2D.linearVelocity = initSpot.right * _speedMultiplier * weaponStats.Speed;
    }

    protected virtual void Destroy()
    {
        CancelInvoke();
        OnDestroyed();
        gameObject.SetActive(false);
    }
}