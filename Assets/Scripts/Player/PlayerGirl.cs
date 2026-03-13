using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(WeaponCollector))]
[RequireComponent(typeof(PlayerKiller))]
[RequireComponent(typeof(PlayerRotator))]
[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(PlayerHealth))]
public class PlayerGirl : MonoBehaviour
{
    [SerializeField] private float _speed;

    private PlayerHealth _health;
    private PlayerAnimator _animator;
    private Rigidbody2D _rigidbody2D;
    private WeaponCollector _weaponCollector;
    private InputReader _playerInput;
    private PlayerKiller _playerKiller;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<PlayerAnimator>();
        _weaponCollector = GetComponent<WeaponCollector>();
        _playerInput = GetComponent<InputReader>();
        _health = GetComponent<PlayerHealth>();
        _playerKiller = GetComponent<PlayerKiller>();
    }

    private void OnEnable()
    {
        _health.OnDead += _playerKiller.Die;
    }

    private void OnDisable()
    {
        _health.OnDead -= _playerKiller.Die;
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
            _weaponCollector.ShootCurrentWeapon(_playerInput.IsMoving);
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
        if (_playerInput.IsFourthWeapon)
        {
            _weaponCollector.SwitchToWeapon(3);
        }
    }
}