using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GunRotator))]
public class WeaponCollector : MonoBehaviour
{
    [SerializeField] private Transform _rightHand;
    [SerializeField] private Transform _leftHand;

    [Header("Все пушки по одной")]
    [SerializeField] private SniperRiffle _rifflePrefab;
    [SerializeField] private Shotgun _shotgunPrefab;
    [SerializeField] private Granader _granaderPrefab;

    private List<Weapon> _weapons = new List<Weapon>();

    private Weapon _firstWeapon;
    private GunRotator _gunRotator;

    private void Awake()
    {
        _gunRotator = GetComponent<GunRotator>();

        if (_rifflePrefab != null) _weapons.Add(Instantiate(_rifflePrefab));
        if (_shotgunPrefab != null) _weapons.Add(Instantiate(_shotgunPrefab));
        if (_granaderPrefab != null) _weapons.Add(Instantiate(_granaderPrefab));
    }

    private void Start()
    {
        EquipWeapon(2);
    }

    private void EquipWeapon(int index)
    {
        if (index >= 0 && index < _weapons.Count)
        {
            _weapons[index].Equip(_rightHand.position, _rightHand);
            _firstWeapon = _weapons[index];
            _gunRotator.SetGunSprite(_firstWeapon.GetGunSprite());
        }
    }

    public void ShootCurrentWeapon()
    {
        _firstWeapon.Shoot();
    }
}