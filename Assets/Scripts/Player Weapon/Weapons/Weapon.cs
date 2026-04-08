using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] public Transform BulletSpawnerSpot;
    [SerializeField] protected ProjectileSpawner ProjectileSpawner;
    [SerializeField] protected WeaponStats WeaponStats;

    public SpriteRenderer GunSprite { get; private set; }

    protected virtual void Awake()
    {
        GunSprite = GetComponent<SpriteRenderer>();
    }

    public virtual void Shoot()
    {
        ProjectileSpawner.Spawn(WeaponStats, BulletSpawnerSpot.position, BulletSpawnerSpot.right);
    }
}