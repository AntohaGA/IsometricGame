using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(WeaponCollector))] // Убедись, что компонент называется именно так
[RequireComponent(typeof(PlayerKiller))]
[RequireComponent(typeof(PlayerRotator))]
[RequireComponent(typeof(InputReader))]
public class PlayerGirl : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private PlayerHealth _health;

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
        _playerKiller = GetComponent<PlayerKiller>();
    }

    private void OnEnable()
    {
        // Подписываемся на смерть игрока
        _health.Destroyd += _playerKiller.Die;
    }

    private void OnDisable()
    {
        // Отписываемся, чтобы избежать ошибок
        _health.Destroyd -= _playerKiller.Die;
    }

    private void Update()
    {
        // --- Движение ---
        Vector2 moveVelocity = _playerInput.MoveInput * _speed;
        _rigidbody2D.linearVelocity = moveVelocity; // Используем velocity вместо linearVelocity

        // --- Анимация ---
        if (_playerInput.IsMoving)
        {
            _animator.Run();
        }
        else
        {
            _animator.Stand();
        }

        // --- Стрельба ---
        if (_playerInput.IsFirstWeaponShoot)
        {
            _weaponCollector.ShootCurrentWeapon();
        }

        // --- Переключение оружия ---
        // Теперь индексы должны соответствовать порядку в массиве WeaponCollector:
        // 0 - Винтовка, 1 - Дробовик, 2 - Миномет, 3 - Гранатомет (или другой порядок, который ты настроил)

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
            // Важно: Если в WeaponCollector гранатомет идет после основного массива,
            // индекс должен быть равен длине массива оружия.
            // В нашем случае массив _weaponPrefabs имеет длину 3 (Rifle, Shotgun, MineLauncher).
            // Значит, индекс для Granader будет 3.
            _weaponCollector.SwitchToWeapon(3);
        }
    }
}