using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] public Transform BulletSpawnerSpot;
    [SerializeField] protected BulletSpawner spawner;
    [SerializeField] protected WeaponStats bulletData;

    public SpriteRenderer GunSprite { get; private set; }

    protected virtual void Awake()
    {
        GunSprite = GetComponent<SpriteRenderer>();
        ValidateComponents();

        if (bulletData != null)
            bulletData.ApplyBaseValues();
    }

    protected virtual void ValidateComponents()
    {
        if (spawner == null) Debug.LogError($"{name}: Spawner не назначен!", this);
        if (BulletSpawnerSpot == null) Debug.LogError($"{name}: BulletSpawnerSpot не назначен!", this);
        if (bulletData == null) Debug.LogError($"{name}: BulletData не назначена!", this);
    }

    public virtual void Shoot()
    {
        spawner.Shoot(BulletSpawnerSpot, bulletData);
    }
}