using UnityEngine;

[RequireComponent(typeof(SingleBulletSpawner))]
[RequireComponent(typeof(SpriteRenderer))]
public class Rifle : Weapon
{
    protected override void Awake()
    {
        base.Awake();
        OverrideBulletData();
    }

    private void OverrideBulletData()
    {
        if (bulletData != null)
        {
            bulletData.finalLifeTime = bulletData.baseLifeTime;
            bulletData.finalSpeed = bulletData.baseSpeed;
            bulletData.finalDamage = bulletData.baseDamage;
        }
    }
}