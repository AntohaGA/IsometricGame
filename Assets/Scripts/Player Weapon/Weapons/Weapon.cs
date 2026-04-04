using UnityEngine;
using System;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] public Transform BulletSpawnerSpot; // Точка вылета (можно переименовать в просто Spot)
    [SerializeField] protected ProjectileSpawner ProjectileSpawner; // ЕДИНСТВЕННЫЙ спавнер!
    [SerializeField] protected WeaponStats WeaponStats;

    public SpriteRenderer GunSprite { get; private set; }
    public event Action OnShoot;

    protected virtual void Awake()
    {
        GunSprite = GetComponent<SpriteRenderer>();
    }

    public virtual void Shoot()
    {
        ProjectileSpawner.Spawn(WeaponStats, BulletSpawnerSpot.position, BulletSpawnerSpot.right);

        TriggerShootEvent();
    }

    protected void TriggerShootEvent()
    {
        OnShoot?.Invoke();
    }
}