
[System.Serializable]
public class BulletConfig
{
    public int damage;
    public int speed;
    public int lifeTime;
    public int penetration;

    public BulletConfig(BulletStats stats, WeaponStats weaponStats)
    {
        damage = stats.DamageMultiplier * weaponStats.Damage;
        speed = stats.SpeedMultiplier * weaponStats.Speed;
        lifeTime = stats.LifeTimeMultiplier * weaponStats.LifeTime;
        penetration = stats.PenetrationMultiplier * weaponStats.Penetration;
    }
}