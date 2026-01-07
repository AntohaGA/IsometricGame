using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SniperRiffle : Weapon
{
    private static WaitForSeconds _timeLifeEffect = new WaitForSeconds(0.3f);

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private IEnumerator Effect()
    {
        GameObject activeShot = Instantiate(_bulletExplosion, _bulletSpawner.position, _bulletSpawner.rotation);

        yield return _timeLifeEffect;

        Destroy(activeShot);
    }

    protected override void Shoot()
    {
        Bullet bullet = Instantiate(_bullet, _bulletSpawner.position, _bulletSpawner.rotation);
        Coroutine coroutineEffect = StartCoroutine(Effect());
    }

    public IEnumerator Reload()
    {
        throw new System.NotImplementedException();
    }
}