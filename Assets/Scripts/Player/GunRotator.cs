using UnityEngine;

public class GunRotator : MonoBehaviour
{
    private Camera _mainCamera;
    private Weapon _gun;

    private void Awake()
    {
        if (_mainCamera == null)
            _mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        RotateTowardsMouse();
    }

    public void SetGun(Weapon weapon)
    {
        _gun = weapon;
    }

    private void RotateTowardsMouse()
    {
        Vector3 mouseWorld = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0;
        Vector2 direction = mouseWorld - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (_gun != null)
        {
            _gun.transform.localRotation = Quaternion.Euler(0, 0, angle);

            bool isFacingRight = angle > -90f && angle < 90f;
            Vector3 scale = _gun.transform.localScale;
            float targetScaleY = isFacingRight ? 1f : -1f;

            scale.y = targetScaleY;
            _gun.transform.localScale = scale;
        }
        else
        {
            Debug.Log("_gun = null");
        }
    }
}