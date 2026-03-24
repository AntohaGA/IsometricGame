using System;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] public Transform BulletSpawnerSpot;
    [SerializeField] protected ProjectileSpawner BulletSpawner;
    [SerializeField] protected WeaponStats WeaponStats;

    public SpriteRenderer GunSprite { get; private set; }

    public event Action OnShoot;

    protected virtual void Awake()
    {
        GunSprite = GetComponent<SpriteRenderer>();
    }

    public virtual void Shoot(bool isMove)
    {
        BulletSpawner.SpawnProjectile(WeaponStats, BulletSpawnerSpot.position, BulletSpawnerSpot.right);
        OnShoot?.Invoke();  // Анимация, звук
    }

    protected void TriggerShootEvent()
    {
        OnShoot?.Invoke();
    }
}