using UnityEngine;

public class GunRotator : MonoBehaviour
{
    private SpriteRenderer _gun;

    private void Update()
    {
        if (_gun != null)
            Rotate();
        else
        {
            Debug.Log("GunRotator - _gun == null");
        }
    }

    public void SetGunSprite(SpriteRenderer gunSprite)
    {
        _gun = gunSprite;
    }

    private void Rotate()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 playerPos = transform.position;
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition) - playerPos;
        float rotate = Mathf.Atan2(mouse.y, mouse.x) * Mathf.Rad2Deg;
        _gun.transform.rotation = Quaternion.Euler(0, 0, rotate);

        if (mousePos.x < transform.position.x)
        {
            _gun.flipY = true;
        }
        else if (mousePos.x > transform.position.x)
        {
            _gun.flipY = false;
        }
    }
}