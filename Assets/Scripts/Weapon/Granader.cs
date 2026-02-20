using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(GrenadeExplosionHandler))]
public class Granader : Weapon
{
    [SerializeField] private GrenadeExplosionHandler _handler;
}