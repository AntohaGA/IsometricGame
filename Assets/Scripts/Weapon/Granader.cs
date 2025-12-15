using System.Collections;
using UnityEngine;

public class Granader : Weapon
{
    [SerializeField] private Grenade _grenade;
    [SerializeField] private float throwSpeed;

    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Shoot();
        }
    }

    public void Shoot()
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

    public Weapon Equip(Vector3 position, Transform parent)
    {
        throw new System.NotImplementedException();
    }

    public void Unequip()
    {
        throw new System.NotImplementedException();
    }

    private void ThrowGrenade(Vector3 targetPosition)
    {
        Grenade grenade = Instantiate(_grenade, transform.position, Quaternion.identity);
        grenade.Launch(targetPosition);
    }

    public SpriteRenderer GetGunSprite()
    {
        throw new System.NotImplementedException();
    }
}