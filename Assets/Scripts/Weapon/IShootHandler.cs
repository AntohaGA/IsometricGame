using UnityEngine;

public interface IShootHandler
{
    void Shoot(Transform from, WeaponStats bulletData);
}