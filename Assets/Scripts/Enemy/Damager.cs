using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour, IAttack
{
    [SerializeField] private int _damagePerHit = 10;
    [SerializeField] private float _damageInterval = 0.5f;
    [SerializeField] private LayerMask _targetLayerMask;

    private Dictionary<IDamagable, float> _lastHitTimes = new();

    public void ApplyDamage(IDamagable target)
    {
        if (target == null) 
            return;

        if (_lastHitTimes.TryGetValue(target, out float lastHitTime))
        {
            if (Time.time - lastHitTime < _damageInterval) 
                return;
        }

        Debug.Log(gameObject.name);

        target.TakeDamage(_damagePerHit);
        _lastHitTimes[target] = Time.time;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!IsInTargetLayer(collision.gameObject))
        {
            return; 
        }

        IDamagable damagable = collision.GetComponentInParent<IDamagable>();

        if (damagable != null)
        {
            ApplyDamage(damagable);
        }
    }

    private bool IsInTargetLayer(GameObject obj)
    {
        return (_targetLayerMask.value & (1 << obj.layer)) > 0;
    }
}