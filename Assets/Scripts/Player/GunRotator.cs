using UnityEngine;

public class GunRotator : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;

    private SpriteRenderer _gunSprite;

    private void Awake()
    {
        if (_mainCamera == null) _mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        if (_gunSprite == null) 
            return;

        Rotate();
    }

    public void SetGunSprite(SpriteRenderer gunSprite) => _gunSprite = gunSprite;

    private void Rotate()
    {
        Vector3 mouseWorld = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0;

        Vector2 direction = mouseWorld - transform.position;
        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        float scaleX = transform.lossyScale.x;

        Debug.Log(scaleX);

        if (scaleX < 0)
        {         
            angle = 360 - angle;
            _gunSprite.flipX = true;
            _gunSprite.flipY = true;
        }
        else
        {
            _gunSprite.flipX = false;
            _gunSprite.flipY = false;
        }

            // Применяем вращение
            _gunSprite.transform.localRotation = Quaternion.Euler(0, 0, angle);
    }
}