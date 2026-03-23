using UnityEngine;

public class GrenadeLauncher : Weapon
{
    [SerializeField] private PoolGrenades _poolGrenades;
    [SerializeField] private PoolExplosions _poolExplosions;

    // ... другие поля типа BulletSpawnerSpot

    public override void Shoot(bool isMove)
    {
        Grenade grenade = _poolGrenades.GetInstance();

        grenade.Destroyed += (proj) => SpawnExplosion(proj.transform.position);

        grenade.Init(WeaponStats, BulletSpawnerSpot.position, BulletSpawnerSpot.right);

        TriggerShootEvent();
    }

    private void SpawnExplosion(Vector3 position)
    {
        GrenadeExplosion explosion = _poolExplosions.GetInstance();
        explosion.transform.position = position;
        explosion.gameObject.SetActive(true);

        // Подписываем взрыв на его собственное уничтожение для возврата в пул
        explosion.GetComponent<Lifetime>().Start(2f); // Время жизни взрыва из его скрипта или конфига

        // Если в GrenadeExplosion нет Lifetime, можно сделать так:
        // explosion.StartCoroutine(ReturnExplosion(explosion, 2f));
    }
}