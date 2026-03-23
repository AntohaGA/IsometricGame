using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Mine : Bullet, ISpawnsOnDestraction
{
    [Header("Mine Settings")]
    [SerializeField] private float _activationDelay;
    [SerializeField] private GameObject _explosion;

    private CircleCollider2D _detectionCollider;

    protected override void Awake()
    {
        base.Awake();
        _detectionCollider = GetComponent<CircleCollider2D>();
    }

    private void OnEnable()
    {
        OnDestroy += SpawnNextObject;
    }

    private void OnDisable()
    {
        OnDestroy -= SpawnNextObject;
    }

    public override void Init(WeaponStats weaponStats, Vector3 spawnPosition, Vector2 shootDirection)
    {
        base.Init(weaponStats, spawnPosition, shootDirection);

        _detectionCollider.enabled = false;
        StartCoroutine(ActivationSequence());
    }

    private IEnumerator ActivationSequence()
    {
        yield return new WaitForSeconds(_activationDelay);

        _detectionCollider.enabled = true;
    }

    public void SpawnNextObject()
    {
        Instantiate(_explosion, transform.position, Quaternion.identity);
    }
}