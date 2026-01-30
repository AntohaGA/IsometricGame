using System;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected Transform _bulletSpawner;

    protected int _damage;

    public event Action<Transform> OnShoot;

    public virtual SpriteRenderer GunSprite => null;

    public virtual void Shoot()
    {
        OnShoot?.Invoke(transform);
    }
}