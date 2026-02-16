// InputReader.cs
using UnityEngine;

[RequireComponent(typeof(PlayerGirl))] // опционально: документация
public class InputReader : MonoBehaviour
{
    private float _horizontal;
    private float _vertical;

    [Header("Debug")]
    [SerializeField] private bool _showInputInLog; // для отладки

    private void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");

        if (_showInputInLog && IsMoving)
        {
            Debug.Log($"Input: {_horizontal}, {_vertical}");
        }
    }

    /// Направление движения (нормализовано)
    public Vector2 MoveInput => new Vector2(_horizontal, _vertical).normalized;

    public bool IsMoving => MoveInput.sqrMagnitude > 0.01f;

    public bool IsFirstWeaponShoot => Input.GetMouseButtonDown(0);

    public bool IsFirstWeapon => Input.GetKeyDown(KeyCode.Alpha1);
    public bool IsSecondWeapon => Input.GetKeyDown(KeyCode.Alpha2);
    public bool IsThirdWeapon => Input.GetKeyDown(KeyCode.Alpha3);
}