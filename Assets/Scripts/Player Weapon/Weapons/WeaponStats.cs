using UnityEngine;

// 1. Добавляем перечисление типов снарядов
public enum ProjectileType { Bullet, Grenade, Mine }

[CreateAssetMenu(fileName = "WeaponStats", menuName = "Weapon/Weapon Data")]
public class WeaponStats : ScriptableObject
{
    // 2. Добавляем поле в ScriptableObject
    public ProjectileType ProjectileType;

    public float LifeTime;
    public int Speed;
    public int Damage;
    public int Penetration;
    // public int MoveAccuracy; // Можно удалить, если не используется
    // public int StopAccuracy; // Можно удалить, если не используется
}