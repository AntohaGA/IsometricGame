using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(WeaponCollector))]
[RequireComponent(typeof(PlayerKiller))]
[RequireComponent(typeof(PlayerRotator))]
[RequireComponent(typeof(InputReader))]
public class PlayerGirl : MonoBehaviour, IDamagable
{
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private float _speed;
    [SerializeField] private float _health;

    private PlayerAnimator _animator;
    private Rigidbody2D _rigidbody2D;
    private WeaponCollector _weaponCollector;
    private InputReader _playerInput;

    public event Action<PlayerGirl> Deceased;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<PlayerAnimator>();
        _weaponCollector = GetComponent<WeaponCollector>();
        _playerInput = GetComponent<InputReader>();
        _healthSlider.value = _health;
    }

    private void Update()
    {
        Vector2 moveVelocity = _playerInput.MoveInput * _speed;
        _rigidbody2D.linearVelocity = moveVelocity;

        if (_playerInput.IsMoving)
        {
            _animator.Run();
        }
        else
        {
            _animator.Stand();
        }

        if (_playerInput.IsFirstWeaponShoot)
        {
            _weaponCollector.ShootCurrentWeapon();
        }

        if (_playerInput.IsFirstWeapon)
        {
            _weaponCollector.SwitchToWeapon(0);
        }
        if (_playerInput.IsSecondWeapon)
        {
            _weaponCollector.SwitchToWeapon(1);
        }
        if (_playerInput.IsThirdWeapon)
        {
            _weaponCollector.SwitchToWeapon(2);
        }
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        _healthSlider.value = _health;

        if (_health <= 0)
        {
            Deceased?.Invoke(this);
        }
    }
}