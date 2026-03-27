using System;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] public Transform BulletSpawnerSpot;
    [SerializeField] protected ProjectileSpawner BulletSpawner; // Для пуль
    [SerializeField] protected PoolObjects<Mine> MineSpawner;   // Для мин (добавили новое поле)
    [SerializeField] protected WeaponStats WeaponStats;

    public SpriteRenderer GunSprite { get; private set; }

    public event Action OnShoot;

    protected virtual void Awake()
    {
        GunSprite = GetComponent<SpriteRenderer>();
    }

    public virtual void Shoot()
    {
        BulletSpawner.SpawnProjectile(WeaponStats, BulletSpawnerSpot.position, BulletSpawnerSpot.right);
        TriggerShootEvent();
    }

    protected void TriggerShootEvent()
    {
        OnShoot?.Invoke();
    }
}