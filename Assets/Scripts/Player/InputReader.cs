using UnityEngine;

public class InputReader : MonoBehaviour
{
    private float _horizontal;
    private float _vertical;

    private void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
    }

    public Vector2 MoveInput => new Vector2(_horizontal, _vertical).normalized;
    public bool IsMoving => MoveInput.sqrMagnitude > 0.01f;
    public bool IsFirstWeaponShoot => Input.GetMouseButtonDown(0);
    public bool IsSecondWeaponShoot => Input.GetMouseButtonDown(1);
    public bool IsFirstWeapon => Input.GetKeyDown(KeyCode.Alpha1);
    public bool IsSecondWeapon => Input.GetKeyDown(KeyCode.Alpha2);
}