using System;
using UnityEngine;

public abstract class Weapon : MonoBehaviour, IDamageDealer
{
    [SerializeField] public Transform BulletSpawnerSpot;
    [SerializeField] protected BulletSpawner BulletSpawner;
    [SerializeField] protected WeaponStats WeaponStats;

    public SpriteRenderer GunSprite { get; private set; }

    public event Action OnShoot;

    protected virtual void Awake()
    {
        GunSprite = GetComponent<SpriteRenderer>();
    }

    public virtual void Shoot(bool isMove)
    {
        BulletSpawner.SpawnBullet(WeaponStats, BulletSpawnerSpot.position, BulletSpawnerSpot.right, isMove);
        OnShoot?.Invoke();  // Анимация, звук
    }
}