using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStats", menuName = "Weapon/Weapon Data")]
public class WeaponStats : ScriptableObject
{
    public int LifeTime;
    public int Speed;
    public int Damage;
    public int Penetration;
    public int MoveAccuracy;
    public int StopAccuracy;
}