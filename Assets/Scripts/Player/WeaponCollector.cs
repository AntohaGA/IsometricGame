using UnityEngine;

[RequireComponent(typeof(GunRotator))]
public class WeaponCollector : MonoBehaviour
{
    [SerializeField] Transform _weaponParent; // Куда крепить оружие
    [SerializeField] private Weapon[] _weaponPrefabs; // Массив обычного оружия (Rifle, Shotgun, Mine)
    [SerializeField] private Granader _granaderPrefab; // Гранатомет - отдельно

    private GunRotator _gunRotator;
    private Weapon _currentWeapon; // Текущее оружие (Rifle/Shotgun/Mine)
    private Granader _currentGranader; // Текущий гранатомет

    private void Awake()
    {
        _gunRotator = GetComponent<GunRotator>();
        SwitchToWeapon(0);
    }

    public void SwitchToWeapon(int index)
    {
        if (_currentWeapon != null) 
            _currentWeapon.gameObject.SetActive(false);

        if (_currentGranader != null)
            _currentGranader.gameObject.SetActive(false);

        if (index < _weaponPrefabs.Length) // Это пушка или мина?
        {
            if (_currentWeapon == null || _currentWeapon.gameObject.scene.name == null)
            {
                _currentWeapon = Instantiate(_weaponPrefabs[index], _weaponParent);
            }
            _currentWeapon.gameObject.SetActive(true);
            _gunRotator.SetGun(_currentWeapon.transform);
            _currentGranader = null; // Сбрасываем гранатомет
        }
        else if (index == _weaponPrefabs.Length && _granaderPrefab != null) // Это гранатомет?
        {
            int granaderIndex = index - _weaponPrefabs.Length; // Если бы массивов было много
            if (_currentGranader == null || _currentGranader.gameObject.scene.name == null)
            {
                _currentGranader = Instantiate(_granaderPrefab, _weaponParent);
            }
            _currentGranader.gameObject.SetActive(true);
            _gunRotator.SetGun(_currentGranader.transform);
            _currentWeapon = null; // Сбрасываем пушку
        }
    }

    public void ShootCurrentWeapon()
    {
        if (_currentWeapon != null)
        {
            _currentWeapon.Shoot();
        }
        else if (_currentGranader != null)
        {
            _currentGranader.Shoot();
        }
    }
}