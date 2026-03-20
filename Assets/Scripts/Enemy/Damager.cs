using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] private float _damagePerHit = 10f;
    [SerializeField] private float _damageInterval = 0.5f;
    [SerializeField] private LayerMask _targetLayerMask;

    private float _lastHitTime;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (IsInTargetLayer(collision.gameObject))
        {
            if (Time.time - _lastHitTime >= _damageInterval)
            {
                Health health = collision.gameObject.GetComponentInParent<Health>();

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