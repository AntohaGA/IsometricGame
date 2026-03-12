using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(SingleBulletSpawner))]
public class Granader : Weapon
{
    [SerializeField] private BulletSpawner _explosionSpawner;


}