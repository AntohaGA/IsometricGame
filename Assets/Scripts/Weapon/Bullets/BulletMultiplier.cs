using UnityEngine;

[CreateAssetMenu(fileName = "BulletStats", menuName = "Weapon/Bullet Data")]
public class BulletMultiplier : ScriptableObject
{
    public int DamageMultiplier =1;
    public int LifeTimeMultiplier=1;
    public int SpeedMultiplier=1;
    public int PenetrationMultiplier=1;
}