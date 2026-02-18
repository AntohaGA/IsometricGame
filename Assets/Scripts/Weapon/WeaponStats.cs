using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStats", menuName = "Weapon/Weapon Data")]
public class WeaponStats : ScriptableObject
{
    [Header("Base Stats")]
    public float baseLifeTime = 3f;
    public float baseSpeed = 10f;
    public float baseDamage = 10f;

    // Пули модифицируют эти значения
    [HideInInspector] public float finalLifeTime;
    [HideInInspector] public float finalSpeed;
    [HideInInspector] public float finalDamage;

    public void ApplyBaseValues()
    {
        finalLifeTime = baseLifeTime;
        finalSpeed = baseSpeed;
        finalDamage = baseDamage;
    }
}