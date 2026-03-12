using UnityEngine;

public class Grenade : Bullet, ISpawnsOnDestraction
{
    [SerializeField] private GrenadeExplosion _explosion;

    private void OnEnable()
    {
        OnDestroy += SpawnNextObject;
    }

    private void OnDisable()
    {
        OnDestroy -= SpawnNextObject;
    }

    public void SpawnNextObject()
    {
        Instantiate(_explosion, transform.position, Quaternion.identity);
    }
}