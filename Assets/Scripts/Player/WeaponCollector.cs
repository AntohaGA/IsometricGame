using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GunRotator))]
public class WeaponCollector : MonoBehaviour
{
    [SerializeField] Transform _firstWeaponSpot;

    [Header("Все пушки по одной")]
    [SerializeField] private Riffle _rifflePrefab;

    private GunRotator _gunRotator;
    private Weapon _currentWeapon;

    private void Start()
    {
        _gunRotator = GetComponent<GunRotator>();

        Weapon w = Instantiate(_rifflePrefab);
        w.gameObject.SetActive(false);
        EquipWeapon(w);
    }

    private void EquipWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;

        SpriteRenderer sprite = weapon.GunSprite;

        _currentWeapon.gameObject.SetActive(true);
        _gunRotator.SetGunSprite(sprite);
        _currentWeapon.transform.SetParent(_firstWeaponSpot);
        _currentWeapon.transform.localPosition = Vector3.zero;
        _currentWeapon.transform.localRotation = Quaternion.identity;
    }

    public void ShootCurrentWeapon()
    {
        _currentWeapon.Shoot();
    }
}