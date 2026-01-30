using UnityEngine;

[RequireComponent(typeof(RiffleBulletSpawner))]
[RequireComponent(typeof(SpriteRenderer))]
public class Riffle : Weapon
{
    private BulletSpawner _riffleBulletSpawner;
    [SerializeField] private float _bulletLifeTime = 2;
    [SerializeField] private float _bulletSpeed = 10;
    [SerializeField] private float _bulletDamage = 50;

    protected SpriteRenderer _spriteRenderer;

    public override SpriteRenderer GunSprite => _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _riffleBulletSpawner = GetComponent<RiffleBulletSpawner>();
    }

    public override void Shoot()
    {
        _riffleBulletSpawner.GetBullet(_bulletLifeTime, _bulletSpeed, _bulletDamage, transform);
    }
}