using UnityEngine;

public class RiffleBulletSpawner : BulletSpawner
{
    private void Start()
    {
        
    }

    public void Init(Weapon weapon)
    {
        weapon.OnShoot += SpawnBullet;
    }

    private void SpawnBullet(Transform transform)
    {

    }
}