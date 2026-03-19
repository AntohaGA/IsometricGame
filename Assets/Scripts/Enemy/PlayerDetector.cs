using System;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField] private ZombieMover _enemyMover;
    [SerializeField] private LayerMask _targetLayerMask;
    [SerializeField] private Collider2D _detectorCollider;

    public event Action<Transform> OnFoundPlayer;

    private void OnEnable()
    {
        _detectorCollider.enabled = true;
    }

    private void OnDisable()
    {
        _detectorCollider.enabled = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!_detectorCollider.enabled)
            return;

        if (IsInTargetLayer(collision.gameObject))
        {
            _detectorCollider.enabled = false;
            OnFoundPlayer?.Invoke(collision.transform);
        }
    }

    private bool IsInTargetLayer(GameObject obj)
    {
        return (_targetLayerMask.value & (1 << obj.layer)) > 0;
    }

    public void Stop()
    {
        _detectorCollider.enabled = false;
    } 
}