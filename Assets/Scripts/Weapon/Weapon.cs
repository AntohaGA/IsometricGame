using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Weapon : MonoBehaviour
{
    [SerializeField] protected Bullet _bullet;
    [SerializeField] protected GameObject _bulletExplosion;
    [SerializeField] protected Transform _bulletSpawner;

    [SerializeField] protected float _reloadTime = 1.5f;
    [SerializeField] protected int _magazineAmmo = 10;
    [SerializeField] protected int _damage = 10;
    [SerializeField] protected float _rateOfFire = 0.5f;

    protected bool _isReloading = false;
    protected int _currentAmmo;

    protected SpriteRenderer _spriteRenderer;

    public event Action Shooted;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _currentAmmo = _magazineAmmo;
    }

    public void Shoot()
    {
        if (_isReloading == false)
        {
            Shooted?.Invoke();
        }
    }

    private void ReloadWeapon()
    {
        StartCoroutine(ReloadCoroutine());
    }

    private void DelayShoot()
    {
        StartCoroutine(DelayCoroutine());
    }

    private IEnumerator ReloadCoroutine()
    {
        _isReloading = true;

        yield return new WaitForSeconds(_reloadTime);

        _isReloading = false;
        _currentAmmo = _magazineAmmo;
    }

    private IEnumerator DelayCoroutine()
    {

        yield return new WaitForSeconds(_rateOfFire);

    }

    public Weapon Equip(Vector3 position, Transform parent)
    {
        Weapon weapon = Instantiate(this, position, Quaternion.identity, parent);

        return weapon;
    }

    public void Unequip()
    {

    }

    public SpriteRenderer GetGunSprite()
    {
        return _spriteRenderer;
    }
}