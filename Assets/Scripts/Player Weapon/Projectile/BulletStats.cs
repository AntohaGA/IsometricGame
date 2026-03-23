using UnityEngine;

[CreateAssetMenu(fileName = "BulletStats", menuName = "Stats/BulletStats")]
public class BulletStats : ScriptableObject
{
    [Header("Multipliers")]
    public int DamageMultiplier = 1;
    public int SpeedMultiplier = 1;
    public int LifeTimeMultiplier = 1;
    public int PenetrationMultiplier = 1;
}