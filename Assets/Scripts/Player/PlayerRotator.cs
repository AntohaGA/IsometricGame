using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerRotator : MonoBehaviour
{
    private SpriteRenderer _playerSprite;
    private Camera _cam;

    private void Awake()
    {
        _playerSprite = GetComponent<SpriteRenderer>();
        _cam = Camera.main;
    }

    private void Update()
    {
        if (_cam == null) return;

        Vector3 mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        bool facingRight = mousePos.x > transform.position.x;

        transform.localScale = new Vector3(facingRight ? 1f : -1f, 1f, 1f);
    }
}