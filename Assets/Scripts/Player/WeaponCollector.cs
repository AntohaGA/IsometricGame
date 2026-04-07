using UnityEngine;

[RequireComponent(typeof(GunRotator))]
public class WeaponCollector : MonoBehaviour
{
    [SerializeField] private Transform _weaponParent; // Точка, где будет висеть оружие
    [SerializeField] private Weapon[] _weaponPrefabs; // Массив префабов из редактора

    private GunRotator _gunRotator;
    private Weapon[] _weaponsInstances; // Массив для хранения созданных экземпляров

    private void Awake()
    {
        _gunRotator = GetComponent<GunRotator>();
        _weaponsInstances = new Weapon[_weaponPrefabs.Length];

        for (int i = 0; i < _weaponPrefabs.Length; i++)
        {
            if (_weaponPrefabs[i] != null)
            {
                _weaponsInstances[i] = Instantiate(_weaponPrefabs[i], _weaponParent);
                _weaponsInstances[i].gameObject.SetActive(false);
            }
        }

        EquipWeapon(0);
    }

    public void EquipWeapon(int index)
    {
        if (index < 0 || index >= _weaponsInstances.Length)
        {
            Debug.LogWarning($"Попытка переключиться на несуществующее оружие: {index}");

            return;
        }

        if (_weaponsInstances[index] == null)
        {
            Debug.LogWarning($"Слот оружия {index} пуст.");

            return;
        }

        foreach (var weapon in _weaponsInstances)
        {
            if (weapon != null)
            {
                weapon.gameObject.SetActive(false);
            }
        }

        _weaponsInstances[index].gameObject.SetActive(true);
        _gunRotator.SetGun(_weaponsInstances[index].transform);
    }

    public void ShootCurrentWeapon()
    {
        // Здесь логика остается прежней: стреляет то, что сейчас активно
        foreach (var weapon in _weaponsInstances)
        {
            if (weapon != null && weapon.gameObject.activeSelf)
            {
                weapon.Shoot();
                break; // Стреляет только одно активное оружие
            }
        }
    }

}