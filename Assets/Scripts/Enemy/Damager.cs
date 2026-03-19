using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] private float _damagePerHit = 10f;
    [SerializeField] private float _damageInterval = 0.5f;
    [SerializeField] private LayerMask _targetLayerMask;
    [SerializeField] private Collider2D _detectorCollider;

    private float _lastHitTime;
    private Collider2D _currentTarget;

    private void OnEnable()
    {
      //  _detectorCollider.enabled = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (IsInTargetLayer(collision.gameObject))
        {
            if (Time.time - _lastHitTime >= _damageInterval)
            {
                if (_currentTarget != collision)
                {
                    _currentTarget = collision;
                }

                Health health = _currentTarget.GetComponentInParent<Health>();

                if (health != null)
                {
                    health.TakeDamage((int)_damagePerHit);
                    Debug.Log($"Нанесено {_damagePerHit} урона!");
                    _lastHitTime = Time.time;
                }
            }
        }
    }

    private bool IsInTargetLayer(GameObject obj)
    {
        return (_targetLayerMask.value & (1 << obj.layer)) > 0;
    }
}