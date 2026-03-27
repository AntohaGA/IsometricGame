public class MineLauncher : Weapon
{
    public override void Shoot()
    {
        Mine mine = MineSpawner.GetInstance();
        mine.Init(WeaponStats, BulletSpawnerSpot.position, BulletSpawnerSpot.right);
        mine.gameObject.SetActive(true);

        TriggerShootEvent();
    }
}