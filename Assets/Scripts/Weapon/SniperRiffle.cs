using System;
using System.Collections;
using UnityEngine;

public class SniperRiffle : Weapon
{
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private IEnumerator Effect()
    {
        GameObject activeShot = Instantiate(_bulletExplosion, _bulletSpawner.position, _bulletSpawner.rotation);

        yield return new WaitForSeconds(0.3f);

        Destroy(activeShot);
    }

    public void Shoot()
    {
        Instantiate(_bullet, _bulletSpawner.position, _bulletSpawner.rotation);
        StartCoroutine(Effect());
    }

    public IEnumerator Reload()
    {
        throw new System.NotImplementedException();
    }

    public Weapon Equip(Vector3 position, Transform parent)
    {
        return Instantiate(this, position, Quaternion.identity, parent);
    }

    public void Unequip()
    {
    }

    public SpriteRenderer GetGunSprite()
    {
        return _spriteRenderer;
    }
}