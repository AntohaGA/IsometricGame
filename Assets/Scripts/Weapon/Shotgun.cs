using UnityEngine;

public class Shotgun : Weapon
{
    [Header("Дробовик настройки")]
    [SerializeField] private int _bulletsCount;
    [SerializeField] private float _spreadAngle;  // разброс в градусах
    [SerializeField] private float _spreadDistance;  // разброс по позиции спавна

    public override void Shoot()
    {

    }
}