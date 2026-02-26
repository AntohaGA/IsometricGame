using UnityEngine;

[CreateAssetMenu(fileName = "BulletStats", menuName = "Weapon/Bullet Data")]
public class BulletStats : ScriptableObject
{
    public float LifeTime;
    public float Speed;
    public float Damage;
    public int CountPenetration;

    public float DamageMultiplier = 1f;
    public int MaxPenetrations = 1;  // Сколько врагов прошивает
    public float DamageFalloff = 1; // 80% урона второму, 64% третьему...
}