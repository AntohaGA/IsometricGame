using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStats", menuName = "Weapon/Weapon Data")]
public class WeaponStats : ScriptableObject
{
    public float LifeTime;
    public float Speed;
    public int Damage;
    public float MoveAccuracy;
    public float StopAccuracy;
}