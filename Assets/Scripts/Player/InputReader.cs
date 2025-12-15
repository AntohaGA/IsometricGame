using UnityEngine;

public class InputReader : MonoBehaviour
{
    private float _horizontal;
    private float _vertical;
    private bool _isShootPressed;
    public Vector2 MoveInput => new Vector2(_horizontal, _vertical).normalized;
    public bool IsMoving => MoveInput.sqrMagnitude > 0.01f;
    public bool IsShoot => Input.GetMouseButtonDown(0);

    private void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
        _isShootPressed = Input.GetMouseButtonDown(0);
    }
}