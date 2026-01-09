using UnityEngine;

public class GunRotator : MonoBehaviour
{
    private SpriteRenderer _gunSprite;

    private void Update()
    {
        if (_gunSprite != null)
        {
            Rotate();
        }
        else
        {
            Debug.Log("GunRotator - _gun == null");
        }
    }

    public void SetGunSprite(SpriteRenderer gunSprite)
    {
        _gunSprite = gunSprite;
    }

    private void Rotate()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        _gunSprite.transform.rotation = Quaternion.Euler(0, 0, angle);

        _gunSprite.flipY = mousePos.x < transform.position.x;
    }
}