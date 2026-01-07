using UnityEngine;

public class GunRotator : MonoBehaviour
{
    private SpriteRenderer _gunSprite;
    private SpriteRenderer _secondGunSprite;

    private void Update()
    {
        if (_gunSprite != null && _secondGunSprite != null)
        {
            Rotate();
        }
        else
        {
            Debug.Log("GunRotator - _gun or _secondGun == null");
        }
    }

    public void SetGunSprite(SpriteRenderer gunSprite)
    {
        _gunSprite = gunSprite;
    }

    public void SetSecondGunSprite(SpriteRenderer secondGunSprite)
    {
        _secondGunSprite = secondGunSprite;
    }

    private void Rotate()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 playerPos = transform.position;
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition) - playerPos;
        float rotate = Mathf.Atan2(mouse.y, mouse.x) * Mathf.Rad2Deg;

        _gunSprite.transform.rotation = Quaternion.Euler(0, 0, rotate);
        _secondGunSprite.transform.rotation = Quaternion.Euler(0, 0, rotate);

        if (mousePos.x < transform.position.x)
        {
            _gunSprite.flipY = true;
            _secondGunSprite.flipY = true;
        }
        else if (mousePos.x > transform.position.x)
        {
            _gunSprite.flipY = false;
            _secondGunSprite.flipY = false;
        }
    }
}