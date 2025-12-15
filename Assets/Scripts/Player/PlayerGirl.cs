using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(WeaponCollector))]
[RequireComponent(typeof(PlayerKiller))]
[RequireComponent(typeof(PlayerRotator))]
[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(PlayerInteractor))]
public class PlayerGirl : MonoBehaviour, IDamagable
{
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private float _speed;
    [SerializeField] private float _health;

    private PlayerAnimator _animator;
    private Rigidbody2D _rigidbody2D;
    private WeaponCollector _weaponCollector;
    private InputReader _playerInput;
    private PlayerInteractor _playerInteractor;

    public event Action<PlayerGirl> Deceased;
    public event Action Shooted;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<PlayerAnimator>();
        _weaponCollector = GetComponent<WeaponCollector>();
        _playerInput = GetComponent<InputReader>();
        _playerInteractor = GetComponent<PlayerInteractor>();
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

        if (_playerInput.IsShoot)
        {
            Debug.Log("Нажал стрелять");
            Shooted?.Invoke();
            _weaponCollector.GetEquipWeapon().Shoot();
        }
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        _healthSlider.value = _health;

        if (_health <= 0)
        {
            Debug.Log("Игрок умер сработало событие");
            Deceased?.Invoke(this);
        }
    }
}