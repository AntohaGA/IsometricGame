using UnityEngine;

public class Grenade : Bullet
{
    [SerializeField] private GameObject _explosion;
    private GrenadeExplosion _explosionSystem;
    private int _explosionRadius;

    protected override void Awake()
    {
        base.Awake();
        _explosionSystem = new GrenadeExplosion(this, _explosion, _explosionRadius);
    }

    protected override void OnDestroyBullet()
    {
        _explosionSystem.Explode();
    }
}