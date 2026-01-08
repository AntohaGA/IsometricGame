using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Weapon : MonoBehaviour
{
    [SerializeField] protected Bullet _bulletPrefab;
    [SerializeField] protected Transform _bulletSpawner;
    [SerializeField] protected int _damage;

    protected SpriteRenderer _spriteRenderer;

    public event Action Shooted;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public virtual void PullTrigger()
    {
            Shooted?.Invoke();
    }

    protected virtual void Shoot()
    {
        Bullet bullet = Instantiate(_bulletPrefab, _bulletSpawner.position, _bulletSpawner.rotation);
        bullet.Fly(1, 10, 50);
    }

    public virtual void Equip(Vector3 position, Transform parent)
    {
        transform.SetParent(parent);
        transform.position = position;
        gameObject.SetActive(true);
    }

    public void Unequip()
    {

    }

    public SpriteRenderer GetGunSprite() => _spriteRenderer;
}