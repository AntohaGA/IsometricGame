using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected Bullet _bulletPrefab;
    [SerializeField] protected Transform _bulletSpawner;
    [SerializeField] protected int _damage;

    public event Action<Transform> OnShoot;

    public virtual SpriteRenderer GunSprite => null;

    public virtual void Shoot()
    {
        OnShoot?.Invoke(transform);
    }
}