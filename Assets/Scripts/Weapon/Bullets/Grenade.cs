using UnityEngine;

public class Grenade : Bullet
{
    [SerializeField] private ExplosionEffect _explosionEffect;
    private float _explosionRadius = 2f;

    private void Start()
    {
        Destroyed += OnGrenadeDestroyed;
    }

    private void OnGrenadeDestroyed(Bullet bullet)
    {
        bullet.Destroyed -= OnGrenadeDestroyed;
        _explosionEffect.Explode(transform.position, _explosionRadius, Damage, this);
        Destroy();
    }
}