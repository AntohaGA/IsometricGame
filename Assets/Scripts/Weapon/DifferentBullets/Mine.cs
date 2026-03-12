using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Mine : Bullet
{
    [Header("Mine Settings")]
    [SerializeField] private float _activationDelay = 2f;
    [SerializeField] private GameObject _explosion;

 //   private bool _isActive;
    private CircleCollider2D _detectionCollider;

    protected override void Awake()
    {
        base.Awake();
        _detectionCollider = GetComponent<CircleCollider2D>();
    }

    public override void Init(WeaponStats weaponStats, Vector3 spawnPosition, Vector2 shootDirection)
    {
        _detectionCollider.enabled = false;
        transform.position = spawnPosition;
       // _rigidbody2D.bodyType = RigidbodyType2D.Static;
      //  _isActive = false;

        StartCoroutine(ActivationSequence());
    }

    private IEnumerator ActivationSequence()
    {
        yield return new WaitForSeconds(_activationDelay);

     //   _isActive = true;
        _detectionCollider.enabled = true;
    }
}