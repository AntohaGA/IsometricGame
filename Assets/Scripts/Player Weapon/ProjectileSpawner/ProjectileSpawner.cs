using UnityEngine;

public abstract class ProjectileSpawner : MonoBehaviour
{
    public abstract void Spawn(WeaponStats stats, Vector3 position, Vector2 direction);
}