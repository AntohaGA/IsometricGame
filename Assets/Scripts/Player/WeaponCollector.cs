using System;
using UnityEngine;

[RequireComponent(typeof(GunRotator))]
public class WeaponCollector : MonoBehaviour
{
    [SerializeField] Transform _firstWeaponSpot;
    [SerializeField] private Rifle _riflePrefab;
    [SerializeField] private Weapon _shotgunPrefab;
    [SerializeField] private Weapon _granaderPrefab;

    private GunRotator _gunRotator;
    private Weapon[] _weapons;
    private Weapon _currentWeapon;

    private void Awake()
    {
        _gunRotator = GetComponent<GunRotator>();
    }

    private void Start()
    {
        _weapons = new Weapon[]
        {
            _riflePrefab != null ? Instantiate(_riflePrefab, _firstWeaponSpot.transform) : null,
            _shotgunPrefab != null ? Instantiate(_shotgunPrefab, _firstWeaponSpot.transform) : null,
            _granaderPrefab != null ? Instantiate(_granaderPrefab, _firstWeaponSpot.transform) : null
        };

        foreach (Weapon weapon in _weapons)
        {
            if (weapon != null)
                weapon.gameObject.SetActive(false);
        }

        Debug.Log(_weapons[0]);

        EquipWeapon(_weapons[0]);
    }

    private void EquipWeapon(Weapon weapon)
    {
        if (weapon == null)
            return;

        if (_currentWeapon != null)
        {
            _currentWeapon.gameObject.SetActive(false);
        }

        _currentWeapon = weapon;
        _currentWeapon.gameObject.SetActive(true);
        _currentWeapon.transform.SetParent(_firstWeaponSpot);
        _currentWeapon.transform.localPosition = Vector3.zero;
        _currentWeapon.transform.localRotation = Quaternion.identity;
        _gunRotator.SetGun(_currentWeapon);
    }

    public void SwitchToWeapon(int index)
    {
        if (index < 0 || index >= _weapons.Length)
        {
            Debug.LogWarning($"Invalid weapon index: {index}");
            return;
        }

        Weapon newWeapon = _weapons[index];

        if (newWeapon == null)
        {
            Debug.Log($"Weapon slot {index} is empty");
            return;
        }

        if (_currentWeapon != newWeapon)
        {
            EquipWeapon(newWeapon);
            Debug.Log($"Switched to weapon {index + 1}");
        }
    }

    public void ShootCurrentWeapon(bool isMoving)
    {
        _currentWeapon.Shoot(isMoving);
    }
}