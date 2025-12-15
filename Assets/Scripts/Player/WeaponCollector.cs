using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GunRotator))]
public class WeaponCollector : MonoBehaviour
{
    [SerializeField] private Transform _positionCurrentWeapon;
    [SerializeField] private SniperRiffle _riffle;
    [SerializeField] private Shotgun _shotgun;
    [SerializeField] private List<Weapon> _weapons;

    private Weapon _currentWeapon;
    private GunRotator _gunRotator;

    public event Action<Weapon> WeaponEquipped;

    private void Awake()
    {
        _gunRotator = GetComponent<GunRotator>();
        _weapons = new List<Weapon>();
        _weapons.Add(_riffle);
        _weapons.Add(_shotgun);
        EquipWeapon(1);
    }

    private void EquipWeapon(int index)
    {
      //  _currentWeapon = _weapons[index].Equip(_positionCurrentWeapon.position, transform);
        WeaponEquipped?.Invoke(_currentWeapon);
        _gunRotator.SetGunSprite(_currentWeapon.GetGunSprite());
    }

    public Weapon GetEquipWeapon()
    {
        return _currentWeapon;
    }
}