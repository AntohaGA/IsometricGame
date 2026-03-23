using UnityEngine;

public class MineLauncher : Weapon
{
    [SerializeField] private PoolObjects<Mine> _poolMines;

    public override void Shoot(bool isMove)
    {
        Mine mine = _poolMines.GetInstance();
        mine.Init(WeaponStats, BulletSpawnerSpot.position, BulletSpawnerSpot.right);
        mine.gameObject.SetActive(true);

        TriggerShootEvent();
    }
}