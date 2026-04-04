using UnityEngine;
[RequireComponent(typeof(GunRotator))]
public class WeaponCollector : MonoBehaviour
{
    [SerializeField] Transform _weaponParent;
    [SerializeField] private Weapon[] _weaponPrefabs; // Теперь здесь ВСЕ оружие

    private GunRotator _gunRotator;
    private Weapon _currentWeapon;

    private void Awake()
    {
        _gunRotator = GetComponent<GunRotator>();
        EquipWeapon(0);
    }

    public void EquipWeapon(int index)
    {
        if (index < 0 || index >= _weaponPrefabs.Length) return;

        if (_currentWeapon != null)
        {
            _currentWeapon.gameObject.SetActive(false);
        }

        if (_currentWeapon == null || _currentWeapon.gameObject.scene.name == null)
        {
            _currentWeapon = Instantiate(_weaponPrefabs[index], _weaponParent);
        }

        _currentWeapon.gameObject.SetActive(true);
        _gunRotator.SetGun(_currentWeapon.transform);
    }

    public void ShootCurrentWeapon()
    {
        if (_currentWeapon != null)
        {
            _currentWeapon.Shoot(); // Просто вызываем Shoot у любого оружия!
        }
    }
}