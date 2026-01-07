using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GunRotator))]
public class WeaponCollector : MonoBehaviour
{
    [SerializeField] private Transform _rightHand;
    [SerializeField] private Transform _leftHand;

    [Header("Все пушки по одной")]
    [SerializeField] private SniperRiffle _riffle;
    [SerializeField] private Shotgun _shotgun;
    [SerializeField] private Granader _granader;

    private List<Weapon> _weapons;

    private Weapon _firstWeapon;
    private Weapon _secondWeapon;
    private GunRotator _gunRotator;

    public event Action<Weapon> FirstWeaponEquipped;
    public event Action<Weapon> SecondWeaponEquipped;

    private void Awake()
    {
        _gunRotator = GetComponent<GunRotator>();
        _weapons = new List<Weapon>
        {
            _riffle,
            _shotgun,
            _granader,
        };
    }

    private void Start()
    {
        EquipWeapon(1);
        EquipSecondWeapon(2);
    }

    private void EquipWeapon(int index)
    {
        _firstWeapon = _weapons[index].Equip(_rightHand.position, transform);
        FirstWeaponEquipped?.Invoke(_firstWeapon);
        _gunRotator.SetGunSprite(_firstWeapon.GetGunSprite());
    }

    private void EquipSecondWeapon(int index)
    {
        _secondWeapon = _weapons[index].Equip(_leftHand.position, transform);
        SecondWeaponEquipped?.Invoke(_secondWeapon);
        _gunRotator.SetSecondGunSprite(_secondWeapon.GetGunSprite());
    }

    public Weapon GetCurrentWeapon()
    {
        return _firstWeapon;
    }

    public Weapon GetSecondWeapon()
    {
        return _secondWeapon;
    }
}