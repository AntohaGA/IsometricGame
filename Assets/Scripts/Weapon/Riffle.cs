using UnityEngine;

[RequireComponent(typeof(RiffleBulletSpawner))]
[RequireComponent(typeof(SpriteRenderer))]
public class Riffle : Weapon
{
    private BulletSpawner _riffleBulletSpawner;

    protected SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _riffleBulletSpawner = GetComponent<RiffleBulletSpawner>();
    }

    public override void Shoot()
    {
        _riffleBulletSpawner.GetBullet(2,10,50, transform);
    }

    public override SpriteRenderer GunSprite => _spriteRenderer;
}