using System.Collections;
using UnityEngine;

public class Granader : Weapon
{
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    public override void PullTrigger()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPos = _camera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, _camera.nearClipPlane));

        // Коррекция по Z, если 2D (по нужной плоскости)
        worldPos.z = 0f;

        ThrowGrenade(worldPos);
    }

    public IEnumerator Reload()
    {
        throw new System.NotImplementedException();
    }

    private void ThrowGrenade(Vector3 targetPosition)
    {
        Bullet bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
        bullet.Fly(2,5,50);
    }
}